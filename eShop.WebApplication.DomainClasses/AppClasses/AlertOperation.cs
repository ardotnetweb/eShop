using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.DomainClasses.AppClasses
{
    public static class AlertOperation
    {
        public static string SurveyOperation(StatusOperation status)
        {
            string result = string.Empty;
            switch (status)
            {
                case StatusOperation.SuccsessInsert:
                    result = "عملیات ذخیره سازی رکورد جدید با موفقیت صورت گرفت";
                    break;
                case StatusOperation.FailInsert:
                    result = "متاسفانه در عملیات ذخیره سازی رکورد جدید خطای وجود دارد . لطفا دوباره تلاش کنید و در صورت تکرار بیش از چند بار به توسعه دهنده سیستم اطلاع دهید";
                    break;
                case StatusOperation.SuccsessUpdate:
                    result = "عملیات ویرایش رکورد با موفقیت صورت گرفت";
                    break;
                case StatusOperation.FailUpdate:
                    result = "متاسفانه در عملیات ویرایش رکورد جدید خطای وجود دارد . لطفا دوباره تلاش کنید و در صورت تکرار بیش از چند بار به توسعه دهنده سیستم اطلاع دهید";
                    break;
                case StatusOperation.SuccsessDelete:
                    result = "عملیات حذف رکورد با موفقیت صورت گرفت";
                    break;
                case StatusOperation.FailDelete:
                    result = "متاسفانه در عملیات حذف رکورد خطای وجود دارد . لطفا دوباره تلاش کنید و در صورت تکرار بیش از چند بار به توسعه دهنده سیستم اطلاع دهید";
                    break;
                case StatusOperation.DuplicateInsert:
                    result = "این نام قبلا در پایگاه داده ذخیره شده است";
                    break;
                case StatusOperation.DuplicateAddress:
                    result = "این آدرس قبلا در پایگاه داده ثبت شده است";
                    break;
                case StatusOperation.DuplicateEmail:
                    result = "این ایمیل قبل در پایگاه داده ثبت شده است";
                    break;
                case StatusOperation.DuplicateName:
                    result = "این نام قبلا در پایگاه داده ثبت شده است";
                    break;
                case StatusOperation.SizeImage:
                    result = "حجم تصویر ارسالی شما بیش از حد تعین شده می باشد.لطفا دوباره بررسی کنید";
                    break;
                case StatusOperation.Dependencies:
                    result = "بخش های دیگری از این سیستم به این بخش وابستگی دارند. لطفا در صورت تمایل برای حذف ابتدا قسمت های وابسته را حذف نمایید سپس این بخش را حذف نمایید";
                    break;
                case StatusOperation.WeightUploadFile:
                    result = "در عملیات آپلود فایل مشکلی به وجود آمده است .. به حجم فایل آپلود شده دقت نمایید";
                    break;
                case StatusOperation.FailUploadFile:
                    result = "در عملیات آپلود فایل مشکلی ایجاد شده است .. لطفا دوباره تلاش کنید .. در صورت تکرار بیش از چند بار حتما توسعه دهنده سیستم را مطلع کنید";
                    break;
                case StatusOperation.NoItem:
                    result = "هیچ آیتمی ثبت نشده است";
                    break;
                case StatusOperation.SuccessSendEmail:
                    result = "نامه الکترونیکی شما با موفقیت ارسال شد";
                    break;
                case StatusOperation.FailSendEmail:
                    result = "در ارسال نامه الکترونیکی شما مشکلی ایجاد شده است . لطفا دقایقی دیگر تلاش کنید";
                    break;
                case StatusOperation.DeleteInsertElmah:
                    result = "ذخیره سازی اینترنت پروتکل های انتخاب شده و حذف ملبقی استثنا ها با موفقیت صورت گرفت";
                    break;
                case StatusOperation.ExceptionReciveSMS:
                    result = "در زمان دریافت اطلاعات از سرویس خطایی به وجود آمده است.. لطفا دقایقی دیگر صفحه را بروز رسانی نمایید";
                    break;
                case StatusOperation.FailSend:
                    result = "متاسفانه در زمان ارسال اطلاعات خطایی به ایجاد شده است لطفا دقایقی دیگر تلاش نمایید";
                    break;
                case StatusOperation.SuccessSend:
                    result = "ارسال پیام با موفقیت صورت گرفت";
                    break;
                case StatusOperation.SuccessCreateUser:
                    result = "ثبت نام شما با موفقیت صورت گرفت برای ورود به سیستم از منوی ناوبری بالا استفاده نمایید";
                    break;
                case StatusOperation.FailCreateUser:
                    result = "در زمان ایجاد حساب کاربری شما خطایی رخ داده شده است لطفا دوباره این کار را انجام دهید";
                    break;
                case StatusOperation.InValidCreateUser:
                    result = "برای ایجاد حساب کاربری خود لطفا از مقادیر معتبر استفاده نمایید";
                    break;
                case StatusOperation.BlockAccount:
                    result = "اکانت کاربری شما مسدود شده است  / برای اطلاعات بیشتر با مدیریت سامانه تماس بگیرید";
                    break;
                case StatusOperation.Invalid:
                    result = "لطفا مقادیر را به صورت صحیح وارد نمایید";
                    break;
                case StatusOperation.ResetPassword:
                    result = "رمز عبور شما با موفقیت ریسیت شد یک لینک  فعال سازی به ایمیل شما ارسال شد";
                    break;
                case StatusOperation.NoExistEmail:
                    result = "چنین رایانامه ای در سیستم ثبت نشده است";
                    break;
                case StatusOperation.ResetPasswordConfirmation:
                    result = "رمز عبور شما با موفقیت ویرایش شد / از منوی ناوبری بالا ورود به سیستم را انتخاب نمایید";
                    break;
                case StatusOperation.ResetPasswordError:
                    result = "متاسفانه در ویرایش رمز عبور خطایی رخ داده است";
                    break;
                case StatusOperation.NoExistUser:
                    result = "کاربری با این مشخصات در سیستم ثبت نشده است";
                    break;
                case StatusOperation.ChangePasswordError:
                    result = "متاسفانه  در زمان تغیر رمز عبور خطایی رخ داده شده است";
                    break;
                case StatusOperation.SuccessChangePassword:
                    result = "تغیر رمز عبور شما با موفقیت صورت گرفت";
                    break;
                case StatusOperation.AddFavoriteError:
                    result = "به لیست علاقه مندی اضافه نشد";
                    break;
                case StatusOperation.SuccessSendComment:
                    result = "نظر شما با موفقیت ارسال شد بعد از تایید مدیریت در سایت نمایش داده می شود";
                    break;
                case StatusOperation.FailSendComment:
                    result = "در زمان ارسال نظر شما خطایی رخ داده است . لطفا دوباره تلاش نمایید";
                    break;
                case StatusOperation.DisableUser:
                    result = "این اکانت در سیستم غیر فعال شده است";
                    break;
                case StatusOperation.FailLogin:
                    result = " از صحت اطلاعات ورودی اطمینان حاصل نمایید";
                    break;
                case StatusOperation.SuccessLogin:
                    result = "ورود کاربر به سیستم با موفقیت انجام شد";
                    break;
                case StatusOperation.NoExistproduct:
                    result = "محصولی برای این گروه آموزشی ثبت نشده است";
                    break;

            }
            return result;
        }
    }


    public static class AlertOperationSendSMS
    {
        public static string SurveyOperation(StatusOperationSendMessage status)
        {
            string result = string.Empty;
            switch (status)
            {
                case StatusOperationSendMessage.InvalidUserNameOrPassword:
                    result = "نام کاربری یا رمز عبور شما اشتباه می باشد";
                    break;
                case StatusOperationSendMessage.SuccessOperation:
                    result = "عملیات ارسال با موفقیت انجام شد";

                    break;
                case StatusOperationSendMessage.NotEnoghCreadit:
                    result = "اعتبار حساب شما کافی نمی باشد";

                    break;
                case StatusOperationSendMessage.DailySendLimit:
                    result = "محدودیت در ارسال روزانه پیام کوتاه";

                    break;
                case StatusOperationSendMessage.WightSendLimit:
                    result = "محدودیت در حجم ارسال پیام کوتاه";

                    break;
                case StatusOperationSendMessage.NotValidNumber:
                    result = "شماره مقصد معتبر نمی باشد";

                    break;
                case StatusOperationSendMessage.UpgradeSoftwer:
                    result = "سامانه در حال بروز رسانی است";

                    break;
                case StatusOperationSendMessage.FilterWord:
                    result = "در پیام از کلامات فیلتر شده استفاده شده است";

                    break;
                case StatusOperationSendMessage.NotActiveUser:
                    result = "کاربر در سیستم فعال نمی باشد";

                    break;
                case StatusOperationSendMessage.NotSend:
                    result = "عدم موفقیت در ارسال پیام کوتاه";

                    break;
                case StatusOperationSendMessage.NotCompleteRegistrationPaper:
                    result = "مدارک کاربر کامل نمی باشد";
                    break;
                case StatusOperationSendMessage.RecId:
                    result = "پیام شما با موفقیت ارسال شد";
                    break;
            }
            return result;
        }
    }
}
