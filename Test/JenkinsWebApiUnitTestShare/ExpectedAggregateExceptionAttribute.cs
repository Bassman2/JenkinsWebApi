namespace JenkinsTest;

    /// <summary>
    /// Attribute that specifies to expect an exception of the specified type
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class ExpectedAggregateExceptionAttribute : ExpectedExceptionBaseAttribute
    {
	/// <summary>
	/// Gets a value indicating the Type of the expected exception
	/// </summary>
	public Type ExceptionType { get; private set; }

	/// <summary>
	/// Gets or sets a value indicating whether to allow types derived from the type of the expected exception to qualify as expected
	/// </summary>
	public bool AllowDerivedTypes { get; set; }

	///// <summary>
	///// Gets the message to include in the test result if the test fails due to not throwing an exception
	///// </summary>
	//protected internal override string NoExceptionMessage => string.Format(CultureInfo.CurrentCulture, FrameworkMessages.UTF_TestMethodNoException, new object[2] { ExceptionType.FullName, base.SpecifiedNoExceptionMessage });

	/// <summary>
	/// Initializes a new instance of the <see cref="T:Microsoft.VisualStudio.TestTools.UnitTesting.ExpectedExceptionAttribute" /> class with the expected type
	/// </summary>
	/// <param name="exceptionType">Type of the expected exception</param>
	public ExpectedAggregateExceptionAttribute(Type exceptionType) : this(exceptionType, string.Empty)
	{ }

	/// <summary>
	/// Initializes a new instance of the <see cref="T:Microsoft.VisualStudio.TestTools.UnitTesting.ExpectedExceptionAttribute" /> class with
	/// the expected type and the message to include when no exception is thrown by the test.
	/// </summary>
	/// <param name="exceptionType">Type of the expected exception</param>
	/// <param name="noExceptionMessage">
	/// Message to include in the test result if the test fails due to not throwing an exception
	/// </param>
	public ExpectedAggregateExceptionAttribute(Type exceptionType, string noExceptionMessage) : base(noExceptionMessage)
	{
		if ((object)exceptionType == null)
		{
			throw new ArgumentNullException("exceptionType");
		}
		if (!typeof(Exception).GetTypeInfo().IsAssignableFrom(exceptionType.GetTypeInfo()))
		{
			throw new ArgumentException("The expected exception type must be System.Exception or a type derived from System.Exception.", "exceptionType");
		}
		ExceptionType = exceptionType;
	}

	/// <summary>
	/// Verifies that the type of the exception thrown by the unit test is expected
	/// </summary>
	/// <param name="exception">The exception thrown by the unit test</param>
	protected override void Verify(Exception exception)
        {
		if (exception.GetType() == typeof(AggregateException))
		{
			exception = exception.InnerException;
		}

		Type type = ((object)exception).GetType();
		if (AllowDerivedTypes)
		{
			if (!ExceptionType.GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()))
			{
				RethrowIfAssertException(exception);
				throw new Exception($"Test method threw exception {type.FullName}, but exception {ExceptionType.FullName} or a type derived from it was expected. Exception message: {GetExceptionMsg(exception)}");
			}
		}
		else if ((object)type != ExceptionType)
		{
			RethrowIfAssertException(exception);
			throw new Exception($"Test method threw exception {type.FullName}, but exception {ExceptionType.FullName} was expected. Exception message: {GetExceptionMsg(exception)}");
		}
	}

	private static string GetExceptionMsg(Exception ex)
	{
		StringBuilder stringBuilder = new StringBuilder();
		bool flag = true;
		for (Exception ex2 = ex; ex2 != null; ex2 = ex2.InnerException)
		{
			string text;
			try
			{
				text = ex2.Message;
			}
			catch
			{
				text = $"Failed to get the message for an exception of type {((object)ex2).GetType()} due to an exception.)";
			}
			stringBuilder.Append($"{(flag ? string.Empty : " ---> ")}{((object)ex2).GetType()}: {text}");
			flag = false;
		}
		return stringBuilder.ToString();
	}

}
