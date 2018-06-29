using System;

namespace App.Core.Templating
{
    public interface ITemplate
    {
        /// <summary>
        /// Gets the original template source
        /// </summary>
        string Source { get; }

        /// <summary>
        /// Renders the template in <see cref="Source"/>
        /// </summary>
        /// <param name="model">
        /// The model object which contains the data for the template.
        /// Can be a subclass of <see cref="IDictionary{TKey,TValue}"/>,
        /// a plain class object, or an anonymous type.
        /// </param>
        /// <param name="formatProvider">Provider to use for formatting numbers, dates, money etc.</param>
        /// <returns>The processed template result</returns>
        string Render(object model, IFormatProvider formatProvider);
    }
}
