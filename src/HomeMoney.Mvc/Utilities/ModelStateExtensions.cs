using System;
using HomeMoney.Core.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HomeMoney.Mvc.Utilities
{
  public static class ModelStateExtensions
  {
    public static ResultModel<string> FillDetails(this ModelStateDictionary modelState, ResultModel<string> model)
    {
      if (modelState == null) throw new ArgumentNullException(nameof(modelState));
      if (model == null) throw new ArgumentNullException(nameof(model));
      if (modelState.IsValid) return model;

      foreach (var modelStateKey in modelState.Keys)
      {
        var modelStateVal = modelState[modelStateKey];
        foreach (var error in modelStateVal.Errors)
        {
          var key = modelStateKey;
          var errorMessage = error.ErrorMessage;
          var exception = error.Exception;
          var message = string.IsNullOrWhiteSpace(errorMessage)
            ? exception?.Message
            : errorMessage;
          model.AddError(message, key);
        }
      }

      return model;
    }

    public static ResultModel<string> ToMessageModel(this ModelStateDictionary modelState)
    {
      if (modelState == null) throw new ArgumentNullException(nameof(modelState));
      var model = new ResultModel<string>();
      modelState.FillDetails(model);
      return model;
    }
  }
}