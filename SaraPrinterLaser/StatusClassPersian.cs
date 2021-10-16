
namespace SaraPrinterLaser
{
    public class StatusClass
    {
        public enum ResponseStatus
        {
            Ok = 0x6E,              //Job Done
            Warning = 0x87,        //Job Done, But with Warning    
            Fail = 0x4E            //Job Fail, device don't do it any thing
        }
        public string ReturnDescription { get; set; }
        public ResponseStatus ResponseReturnStatus { get; set; }

        public const string Error = "خطا";
        public const string Question = "سوال";
        public const string Message = "پیغام";
        public const string Dueto = "به دلیل";

        public const string Message_SucsessOpration =
            " عملیات با موفقیت انجام پذیرفت ";
        public const string Message_SucsessTest =
            " تست با موفقیت انجام پذیرفت ";
        public const string Message_SucsessCopy =
            " متن به درستی کپی شد ";
        public const string Message_TrueReceive =
            " ارتباط با دستگاه به درستی برقرار شد ";
        public const string Message_DeviceHaveFlipper =
             " دستگاه داری سیستم چاپ دو طرفه نیست ";
        public const string Message_NewFileCreate =
            " فایل جدید مجدداً بارگذاری شد ";
        public const string Message_LaserPointWarning =
            "کاربر محترم این تست با کمک انسان انجام می شود.لطفاً دکمه مارک را فشرده و بلافاصله تاثیر گذاری لیزر را بر روی کار مشاهده نمایید و در آخر گزینه بله یا خیر را انتخاب نمایید. ";
        public const string Message_PleaseSelectTheImage =
            " لطفا عکس مورد نظر را انتخاب کنید ";



        public const string Question_DoyouLikeReturnParameters =
            "آیا از بازگشت مقادیر به پیشفرض اطمینان دارید؟";
        public const string Question_DyouLikeRemovePen =
            "آیا از حذف قلم اطمینان دارید ؟";
        public const string Question_OverWrite =
             "فایل مورد نظر وجود دارد ، آیا از جایگزین کردن آن اطمینان دارید ؟";
        public const string Question_SaveAndExit =
             "تنظیمات مورد نظر ذخیره نشده است. آیا برای خروج اطمینان دارید ؟";
        public const string Question_SeeMarkPointOntheCard =
            " آیا نقطه حک شده بر روی کارت را مشاهده می کنید ؟ ";

        public const string Warning_CardCapacityWarning =
            " مخزن در حال خالی شدن است، لطفا پس از انجام کار مخزن را پر کنید ";

        public const string Error_RetryTest =
             " تست به درستی انجام نشد، لطفا دوباره تلاش نمایید. ";
        public const string Error_SelectPenName =
             " لطفا نام قلم مورد نظر را به درستی وارد نمایید ";
        public const string Error_CRNotFound =
             " ماژول کارتخوان یافت نشد ";
        public const string Error_LaserNotFound =
                " ماژول لیزر یافت نشد ";
        public const string Error_ApplicationDuplicateRun =
            " نرم افزار در جای دیگر باز می باشد، لطفا از بسته بودن آن اطمینان حاصل فرمایید ";
        public const string Error_CFGFileNotFound =
            " فایل های نرم افزار پاک شده است ";
        public const string Error_FailedOpen =
            " ارتباط با دستگاه برقرار نشد ";
        public const string Error_NoDevice =
            " دستگاه وصل نیست و یا اینکه خاموش است لطفا ارتباط را بررسی نمایید ";
        public const string Error_FailCommunication =
            " لطفا پس از تلاش مجدد کابل های ارتباطی را بررسی نمایید ";
        public const string Error_Hardver =
            " ورژن برد دستگاه مناسب نیست، لطفا با پشتیبانی تماس بگیرید ";
        public const string Error_DevCFG =
            " فایل های نرم افزار پاک شده است، لطفا با پشتیبانی تماس بگیرید ";
        public const string Error_StopSignal =
            " دستور توقف دریافت شده است ";
        public const string Error_UserStop =
            " کاربر دستور توقف صادر کرده است ";
        public const string Error_Unknown =
            " دلیل خطا مشخص نیست ";
        public const string Error_OUTTIME =
            " عملیات به دلیل زمان زیاد متوقف شده است، لطفا مجدداً اقدام فرمایید ";
        public const string Error_NoInit =
            "  دستگاه راه اندازی اولیه نشده است و یا دستگاه متصل نمی باشد ";
        public const string Error_ReadFile =
             "فایل مورد نظر معیوب است ، لطفا مجدداً اقدام فرمایید ";
        public const string Error_OWENWNDNULL =
             " ویندوز مشکل پیدا کرده است، لطفا سیستم را ریستارت فرمایید ";
        public const string Error_NOFINDFONT =
             " فونت مورد نظر پیدا نشد، لطفا مجدداً اقدام فرمایید";
        public const string Error_PENNO =
             " شماره قلم مورد نظر اشتباه انتخاب شده است، لطفا مجدداً اقدام فرمایید";
        public const string Error_NOTTEXT =
             " شیء انتخاب شده دارای متن نیست، لطفا مجدداً اقدام فرمایید";
        public const string Error_ImageGenerationFail =
             " هیچ تصویری تولید نشد لطفا اتصال به دستگاه را بررسی کرده و  یا با پشتیبانی تماس حاصل فرمایید ";
        public const string Error_SAVEFILE =
             " ذخیره فایل انجام نشد، لطفا مجدداً اقدام فرمایید";
        public const string Error_NOFINDENT =
             " فایل وارد شده یافت نشد، لطفا مجدداً اقدام فرمایید";
        public const string Error_STATUE =
             " عملیات مورد نظر انجام پذیر نیست، لطفا مجدداً اقدام فرمایید";
        public const string Error_PARAM =
              " تنظیمات وارد شده صحبح نمی باشد، لطفا مجدداً اقدام فرمایید";
        public const string Error_InitFailed =
              " راه اندازی دستگاه با مشکل مواجه شده است و یا دستگاه متصل نمی باشد. ";
        public const string Error_Fail =
             " انجام عملیات با خطا مواجه شد  ";
        public const string Error_CardEmpty =
             " مخزن کارت ندارد، لطفا از داشتن کارت در مخزن اطمینان حاصل نمایید ";
        public const string Error_NoStackerDetect =
            " مخزن در داخل دستگاه نیست، لطفا مخزن را در داخل دستگاه قرار دهید ";
        public const string Error_CardJam =
            " کارت در داخل دستگاه گیر کرده لطفا مسیر شخصی سازی کارت را بررسی نمایید ";
        public const string Error_DeviceBusy =
              " دستگاه در حال انجام دستور قبلی است، لطفا اندکی بعد دوباره سعی کنید ";
        public const string Error_FailDataReceive =
             " لطفا پس از تلاش مجدد کابل های ارتباطی را بررسی نمایید ";
        public const string Error_WithoutFlipper =
             " دستگاه داری سیستم چاپ دو طرفه نیست ";
        public const string Error_InputDataIncorrect =
              " اطلاعات وارد شده معتبر نمی باشد ";
        public const string Error_CRModuleNotHaveCard =
             " هیچ کارتی در  داخل دستگاه کارت خوان نیست ";
        public const string Error_CRWriteOnMagnetFail =
             " نوشتن اطلاعات بر روی نوار مغناطیسی انجام نشد ";
        public const string Error_CRReadOnMagnetFail =
             " خواندن اطلاعات از روی نوار مغناطیسی انجام نشد ";
        public const string Error_CRPermitBehindFail =
            " کارت وارد دستگاه کارت خوان نشد ";
        public const string Error_CRCardEjectFaild =
            " کارت از کارتخوان خارج نشد ";
        public const string Error_CRMagnetWriteModeFail =
            " نوع نوشتن بر روی مغناطیس کارت مشخص نشد ";
        public const string Error_CRPermitAllCardIn =
            " نوع ورودی کارت بدرستی مشخص نشد ";

        public const string Error_CRRFIDDeactivitionFail =
             " غیرفعالسازی کارت انجام نشد ";
        public const string Error_FolderNotFound =
             " پوشه مورد نظر یافت نشد ";
        public const string Error_SensorReadFail =
            " بررسی سنسور های دستگاه کارت خوان ما مشکل مواجه شد ";
        public const string Error_CRRFIDActivitionFail =
            " فعالسازی کارت انجام نشد ";
        public const string Error_CRRFIDNotActivite =
            " کارت فعال نیست ";
        public const string Error_CRSendApduFail =
            " فعالسازی کارت انجام نشد ";
        public const string Error_CRMoveToRFIDFaild =
            " انتقال کارت به سمت کارتخوان بدون سیم با مشکل مواجه شد";
        public const string Error_CRRFIDInquireActivitionFail =
            " بررسی فعالسازی کارت انجام نشد ";
        public const string Error_SerialPortAlreadyIsOpen =
            " پورت ارتباطی مشغول می باشد ";
        public const string Error_NoResponseDetect =
            " پاسخی از دستگاه دریافت نشد ";
        public const string Error_PenWriteFail =
            " قلم مورد نظر ذخیره نشد ";
        public const string Error_PenReadFail =
            " قلم مورد نظر بازیابی نشد ";
        public const string Error_InsertedNumberFail =
            "لطفا اعداد پارامترها به درستی وارد نمایید";
        public const string Error_SelectPenCarfully =
            "لطفا قلم مورد نظر را انتخاب کنید";
        public const string Error_PortNotFound =
             "پورتی برای اتصال یافت نشد لطغا از اتصال دستگاه اطمینان حاصل نمائید";
        public const string Error_MacAddressReadError =
            "آدرس سخت افزاری به درستی خوانده نشد";
        public const string Error_ReadSettingsFail =
            "تنظیمات به درستی بازیابی نشد";
        public const string Error_InputAddressFail =
            "مسیر ذخیره بدرستی وارد نشده است";
        public const string Error_EnterCorrectIp =
           "لطفا آپی آدرس را به درستی وارد نمایید";
        public const string Error_EnterCorrectPort =
           "لطفا پورت را به درستی وارد نمایید";
        public const string Error_FileDeleteFail =
            "فایل به درستی پاک نشد";
        public const string Error_TcpRecevieConnectionFailed =
            " اتصال با شبکه برقرار نشد ";
        public const string Error_NoDataFoundForPrint =
            " اطلاعاتی برای چاپ پیدا نشد ";
        public const string Error_TheReceiveDataFailed =
            " اطلاعات دریافتی معتبر نمی باشد ";
        public const string Error_TheReceiveDataIsFile =
          " اطلاعات دریافتی شامل دستورات نیست بلکه شامل فایل می باشد ";
        public const string Error_TheAllPortLessThanTheMinimum =
        " تفاوت بین پورت آغازین از پورت پایانی کمتر مقدار حداقل می باشد. تعداد حداقل برابر است با : ";
        public const string Error_TheInputPortIsOutOFRange =
            " پورت وارد شده خارج از محدوده مجاز تعیین شده می باشد ";
        public const string Error_TheSerialNumberIsNotWritten =
            " سریال دستگاه قابل خواندن نیست لطفا بازنگری فرمایید ";
        public const string Error_TheSerialNumberIsNotValid =
           " .سریال دستگاه معتبر نیست لطفا بازنگری فرمایید ";
        public const string Error_TheUserNameOrPasswordisInvalid =
           " نام کاربری و یا کلمه عبور وارد شده صحیح نیست. ";
        public const string Error_VerifyPasswordIsnotEqual =
            " کلمه عبور وارد شده یکسان نمی باشد. ";
        public const string Error_PasswordLengthisLessthan =
            " طول کلمه عبور کمتر از شش کاراکتر می باشد. ";
        public const string Error_DatabaseDataEntryIsIncorrect =
            " اطلاعات ورودی در پایگاه داده معتبر نمی باشد لطفا با پشتیبانی تماس بگیرید. ";



        public const string Error_CannotFindCamera =
            " دوربین با اسم مذکور یافت نشد. ";
        public const string Error_CannotChangeWidthResolution =
            " عرض تصویر دوربین تغییرنیافت ";
        public const string Error_CannotChangeBrightness =
            " روشنایی تصویر دوربین تغییرنیافت ";
        public const string Error_CannotChangeHeightResolution =
            " طول تصویر دوربین تغییر نیافت ";
        public const string Error_CannotTakePicture =
            " تصویر از دوربین دریافت نشد ";
        public const string Error_FocusValueEnteredNotValid =
            " عدد وارد شده برای تنظیمات فوکوس صحیح نمی باشد ";
        public const string Error_TrayMoveToGetImageFail =
             " حرکت سینی به سمت دریافت تصویر انجام نشد ";
        public const string Error_whiteLedTurnOnFail =
            " نور سفید روشن نشد ";
        public const string Error_UVLedTurnOnFail =
            " نور ماورای بنفش روشن نشد ";
        public const string Error_CannotTakePictureWhiteLed =
            " تصویر نور سفید دریافت نشد ";
        public const string Error_CannotTakePictureUVLed =
            " تصویر نور ماورای بنفش دریافت نشد ";
        public const string Error_TrayMoveToGetBookletFail =
            " حرکت سینی به سمت دریافت سند انجام نشد ";
        public const string Error_whiteLedTurnOffFail =
           " نور سفید خاموش نشد ";
        public const string Error_UVLedTurnOffFail =
            " نور ماورای بنفش خاموش نشد ";

        public const string Error_TextToImageFunctionFailOccure =
            " :در تبدیل متن به عکس خطای زیر رخ داد ";
        public const string Error_CardItemsTextIsEmpty =
            " داخل آیتم هایی که قرار است چاپ شود متنی وجود ندارد ";
        public const string Error_CardItemsImageIsEmpty =
    " داخل آیتم هایی که قرار است چاپ شود عکسی وجود ندارد ";
        public const string Error_CardDetectiontypeIsIncorrect =
          " نوع کارت به درستی مشخص نشده است ";
        public const string Error_UserAccsessFail =
          " شما به این قسمت دسترسی ندارید، لطفا با مدیر مجموعه تماس حاصل فرمایید ";

        public const string Error_MRZGenerationFailed =
          " اطلاعات وارد شده طبق استاندارد 9303 نمی باشد.MRZ Generation Failed ";

    }
}
