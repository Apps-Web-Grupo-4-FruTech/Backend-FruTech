using FruTech.Backend.API.Shared.Infrastructure.Interfaces.ASP.Configuration.Extensions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace FruTech.Backend.API.Shared.Infrastructure.Interfaces.ASP.Configuration;

/// <summary>
///  Route naming convention that converts controller names to kebab-case
/// </summary>
public class KebabCaseRouteNamingConvention : IControllerModelConvention
{
    /// <summary>
    ///  Replaces the [controller] token in route templates with the kebab-case version of the controller name
    /// </summary>
    /// <param name="selector"></param>
    /// <param name="name"></param>
    private static AttributeRouteModel? ReplaceControllerTemplate(SelectorModel selector, string name)
    {
        return selector.AttributeRouteModel != null ? new AttributeRouteModel
        {
            Template = selector.AttributeRouteModel.Template?.Replace("[controller]", name.ToKebabCase())
        } : null;
    }
    /// <summary>
    ///  Applies the kebab-case naming convention to the controller and its actions
    /// </summary>
    /// <param name="controller"></param>
    public void Apply(ControllerModel controller)
    {
        foreach (var selector in controller.Selectors)
        {
            selector.AttributeRouteModel = ReplaceControllerTemplate(selector, controller.ControllerName);
        }

        foreach (var selector  in controller.Actions.SelectMany(x => x.Selectors))
        {
            selector.AttributeRouteModel = ReplaceControllerTemplate(selector, controller.ControllerName);
        }
    }
}