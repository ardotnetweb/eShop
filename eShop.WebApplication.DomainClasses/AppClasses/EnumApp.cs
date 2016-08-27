using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.AppClasses
{
    public enum Favorite
    {
        CategoryTechnology = 1,
        CategoryeBook = 2
    }


    public enum StatusOperation
    {
        SuccsessInsert,
        FailInsert, SuccsessUpdate, FailUpdate,
        SuccsessDelete, FailDelete, DuplicateInsert, DuplicateAddress,
        DuplicateEmail, DuplicateName, SizeImage, Dependencies, FailUploadFile, WeightUploadFile,
        NoItem, SuccessSendEmail, FailSendEmail, DeleteInsertElmah, ExceptionReciveSMS, FailSend, SuccessSend,
        SuccessCreateUser, FailCreateUser, InValidCreateUser, BlockAccount, Invalid, ResetPassword, NoExistEmail,
        ResetPasswordConfirmation, ResetPasswordError, NoExistUser, ChangePasswordError, SuccessChangePassword,
        AddFavoriteError, SuccessSendComment, FailSendComment, DisableUser, SuccessLogin, FailLogin
        ,NoExistproduct

    }
    public enum AlertMode
    {
        info,
        success,
        warning,
        White
    }
    public enum StatusOperationSendMessage
    {
        InvalidUserNameOrPassword, SuccessOperation, NotEnoghCreadit,
        DailySendLimit, WightSendLimit, NotValidNumber, UpgradeSoftwer, FilterWord,
        NotActiveUser, NotSend, NotCompleteRegistrationPaper, RecId
    }

    public enum StatusRecomend
    {
        SpecialRecomend = 1,
        NormalRecomend = 2
    }
    public enum StatusSearch
    {
        MoreVisited = 1, New = 2, Offer = 3, Price = 4
    }
    public enum StatusOrder
    {
        Ascending = 1, Descending = 2
    }


    public enum StatusTypeOrder
    {
        EWPR = 0, EWPA = 1, EWCO = 2
    }
    public enum StatusTypePay
    {
        Online = 1, PostOffice = 2, BuyDirect = 3
    }

    public enum eChangeFrequency
    {
        always,
        hourly,
        daily,
        weekly,
        monthly,
        yearly,
        never
    }

    public struct Priority
    {
        public const double priority_00 = 0.0D;
        public const double priority_01 = 0.1D;
        public const double priority_02 = 0.2D;
        public const double priority_03 = 0.3D;
        public const double priority_04 = 0.4D;
        public const double priority_05 = 0.5D;
        public const double priority_06 = 0.6D;
        public const double priority_07 = 0.7D;
        public const double priority_08 = 0.8D;
        public const double priority_09 = 0.9D;
        public const double priority_10 = 1.0D;
    }

}
