using System;
using App.Core.Templating;


namespace App.Framework.Templating.Liquid
{
    public interface ILiquidTemplateEngine
    {
        string Render(string source, object model, IFormatProvider formatProvider);
        ITemplate Compile(string source);
    }
}