using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Shared.Enums
{
    public enum OperationStatusCode
    {
        [Display(Name = "عملیات با موفقیت انجام شد")]
        OK = 200,

        [Display(Name = "خطایی در پردازش رخ داد")]
        ServerError = 500,

        [Display(Name = "پارامتر های ارسالی معتبر نیستند")]
        BadRequest = 400,

        [Display(Name = "یافت نشد")]
        NotFound = 404,

        [Display(Name = "خطای احراز هویت")]
        UnAuthorized = 401
    }
}
