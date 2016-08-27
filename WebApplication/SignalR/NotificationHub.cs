using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using eShop.WebApplication.ServiceLayer.AppServices.InterfaceService;
using StructureMap;
using eShop.WebApplication.ServiceLayer.AppServices.ImplamentService;
using eShop.WebApplication.DataLayer.EntityContext;

namespace WebApplication.SignalR
{
    public class NotificationHub : Hub
    {
        private readonly IProductService _productService;
        public NotificationHub()
        {
            _productService = new ProductService(new DataContext());
        }
        public void SendNotification()
        {
            var product = _productService.GetLastRecord();
            Clients.Others.ShowNotification(new { Id = product.Id, Name = product.Name.Replace(' ', '-').ToString() });
        }
    }
}