﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dualp.BitSell.Infrastructure.ModelBinders
{
    public class CurrencyModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType == typeof(decimal) || bindingContext.ModelType == typeof(decimal?))
            {
                var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

                var input = valueResult.AttemptedValue;
                if (!string.IsNullOrEmpty(input) && input.Contains(","))
                {
                    var replace = input.Replace(",", "");
                    if (decimal.TryParse(replace, out var value)) return value;
                    //var modelState = new ModelState { Value = valueResult };
                    //bindingContext.ModelState.Add(bindingContext.ModelName, modelState);
                    //return null;
                }
            }

            return base.BindModel(controllerContext, bindingContext);
        }
    }
}
