using eShop.WebApplication.DomainClasses.ViewModelClasses;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using Directory = System.IO.Directory;
using Version = Lucene.Net.Util.Version;

namespace WebApplication.Lucene
{
    public class LuceneProductSearch
    {
        private const Version _version = Version.LUCENE_30;

        private static readonly string _luceneDir = HttpContext.Current.Server.MapPath("~/App_Data/Lucene_Index");

        private static FSDirectory _directoryTemp;

        public static FSDirectory _directory
        {
            get
            {
                if (_directoryTemp == null)
                    _directoryTemp = FSDirectory.Open(new DirectoryInfo(_luceneDir));
                if (IndexWriter.IsLocked(_directoryTemp))
                    IndexWriter.Unlock(_directoryTemp);
                string lockFilePath = Path.Combine(_luceneDir, "write.lock");
                if (File.Exists(lockFilePath))
                    File.Delete(lockFilePath);
                return _directoryTemp;
            }
        }

        //Section ADD
        public static void AddUpdateLuceneIndex(LuceneProductModel productData)
        {
            AddUpdateLuceneIndex(new List<LuceneProductModel> { productData });
        }

        public static void AddUpdateLuceneIndex(IEnumerable<LuceneProductModel> bookData)
        {
            // init lucene
            var analyzer = new StandardAnalyzer(_version);
            using (var writer = new IndexWriter(_directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                // add data to lucene search index (replaces older entry if any)
                foreach (LuceneProductModel data in bookData)
                    _addToLuceneIndex(data, writer);

                // close handles
                analyzer.Close();
                writer.Optimize();
                writer.Commit();
                writer.Dispose();
            }
        }
        private static void _addToLuceneIndex(LuceneProductModel productData, IndexWriter writer)
        {
            // remove older index entry
            var searchQuery = new TermQuery(new Term("Product_Id", productData.Product_Id.ToString(CultureInfo.InvariantCulture)));
            writer.DeleteDocuments(searchQuery);

            // add new index entry
            var productDocument = new Document();

            // add lucene fields mapped to db fields
            productDocument.Add(new Field("Product_Id",
                productData.Product_Id.ToString(CultureInfo.InvariantCulture), Field.Store.YES, Field.Index.NOT_ANALYZED));

            if (productData.Product_Name != null)
            {
                productDocument.Add(new Field("Product_Name", productData.Product_Name, Field.Store.YES, Field.Index.ANALYZED,
                    Field.TermVector.WITH_POSITIONS_OFFSETS));
            }
            if (productData.Product_Explain != null)
            {
                productDocument.Add(new Field("Product_Explain", productData.Product_Explain, Field.Store.YES, Field.Index.ANALYZED,
                    Field.TermVector.WITH_POSITIONS_OFFSETS));
            }

            // add entry to index
            writer.AddDocument(productDocument);
        }


        //Section Update And Delete
        public static void ClearLuceneIndexRecord(int recordId)
        {
            // init lucene
            var analyzer = new StandardAnalyzer(_version);
            using (var writer = new IndexWriter(_directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                // remove older index entry
                var searchQuery = new TermQuery(new Term("Product_Id", recordId.ToString(CultureInfo.InvariantCulture)));
                writer.DeleteDocuments(searchQuery);
                // close handles
                analyzer.Close();
                writer.Dispose();
            }
        }

        //Section Search
        public static IEnumerable<LuceneProductModel> Search(string input, params string[] fieldsName)
        {
            if (string.IsNullOrEmpty(input))
                return new List<LuceneProductModel>();

            IEnumerable<string> terms = input.Trim().Replace("-", " ").Split(' ')
                .Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Trim() + "*");
            input = string.Join(" ", terms);
            return _search(input, fieldsName);
        }

        private static IEnumerable<LuceneProductModel> _search(string searchQuery, string[] searchFields)
        {
            // validation
            if (string.IsNullOrEmpty(searchQuery.Replace("*", "").Replace("?", "")))
                return new List<LuceneProductModel>();

            // set up lucene searcher
            using (var searcher = new IndexSearcher(_directory, false))
            {
                const int hitsLimit = 1000;
                var analyzer = new StandardAnalyzer(Version.LUCENE_30);


                var parser = new MultiFieldQueryParser
                    (Version.LUCENE_30, searchFields, analyzer);
                Query query = ParseQuery(searchQuery, parser);
                ScoreDoc[] hits = searcher.Search(query, null, hitsLimit, Sort.RELEVANCE).ScoreDocs;

                if (hits.Length == 0)
                {
                    searchQuery = SearchByPartialWords(searchQuery);
                    query = ParseQuery(searchQuery, parser);
                    hits = searcher.Search(query, hitsLimit).ScoreDocs;
                }

                IEnumerable<LuceneProductModel> results = _mapLuceneToDataList(hits, searcher);
                analyzer.Close();
                searcher.Dispose();
                return results;
            }
        }

        private static Query ParseQuery(string searchQuery, QueryParser parser)
        {
            Query query;
            try
            {
                query = parser.Parse(searchQuery.Trim());
            }
            catch (ParseException)
            {
                query = parser.Parse(QueryParser.Escape(searchQuery.Trim()));
            }
            return query;
        }

        private static string SearchByPartialWords(string bodyTerm)
        {
            bodyTerm = bodyTerm.Replace("*", "").Replace("?", "");
            IEnumerable<string> terms = bodyTerm.Trim().Replace("-", " ").Split(' ')
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x => x.Trim() + "*");
            bodyTerm = string.Join(" ", terms);
            return bodyTerm;
        }

        private static IEnumerable<LuceneProductModel> _mapLuceneToDataList(IEnumerable<ScoreDoc> hits,IndexSearcher searcher)
        {
            return hits.Select(hit => _mapLuceneDocumentToData(searcher.Doc(hit.Doc))).ToList();
        }

        private static LuceneProductModel _mapLuceneDocumentToData(Document doc)
        {
            return new LuceneProductModel
            {
                Product_Id = Convert.ToInt32(doc.Get("Product_Id")),
                Product_Name = doc.Get("Product_Name"),
                Product_Explain = doc.Get("Product_Explain"),
            };
        }

    }
}