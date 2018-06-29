using App.Core.Common;
using System;

namespace App.Core.Templating
{
    public interface ITemplateEngine
	{
		/// <summary>
		/// Compiles a template for faster rendering
		/// </summary>
		/// <param name="source">The template source</param>
		/// <returns>Compiled template</returns>
		ITemplate Compile(string source);

		/// <summary>
		/// Directly renders a template source 
		/// </summary>
		/// <param name="source">The template source</param>
		/// <param name="model">
		/// The model object which contains the data for the template.
		/// Can be a subclass of <see cref="IDictionary&lt;string, object&gt;"/>,
		/// a plain class object, or an anonymous type.
		/// </param>
		/// <param name="formatProvider">Provider to use for formatting numbers, dates, money etc.</param>
		/// <returns>The processed template result</returns>
		string Render(string source, object model, IFormatProvider formatProvider);

		/// <summary>
		/// Creates a test model for the passed entity to be used during preview and test.
		/// </summary>
		/// <param name="entity">The entity to create a test model for.</param>
		/// <param name="modelPrefix">The model prefix</param>
		/// <returns>An object which implements <see cref="ITestModel"/> and contains some test data for the declared properties of <paramref name="entity"/></returns>
		ITestModel CreateTestModelFor(BaseEntity entity, string modelPrefix);
	}

	public interface ITestModel
	{
		/// <summary>
		/// The name of the model to use as hash key, e.g. Product, Order, Customer etc.
		/// </summary>
		string ModelName { get; }
	}
}
