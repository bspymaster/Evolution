using System;

public class Alert {
    private string title;
    private string details;
    private DateTime timeStamp;

    /// <summary>
    /// Create a new Alert
    /// </summary>
    /// <param name="title">A short title for the Alert</param>
    /// <param name="details">A longer description about the alert</param>
	public Alert(string title, string details)
    {
        this.title = title;
        this.details = details;
        this.timeStamp = DateTime.Now;
    }
    /// <summary>
    /// Creates a deep copy of a given alert
    /// </summary>
    /// <param name="other">The alert to copy</param>
    public Alert(Alert other)
    {
        title = other.getTitle();
        details = other.getDetails();
        timeStamp = other.getTimeStamp();
    }

    /// <summary>
    /// Get the title of the Alert
    /// </summary>
    /// <returns>The tile of the Alert</returns>
    public string getTitle()
    {
        return title;
    }
    /// <summary>
    /// Get the details of the Alert
    /// </summary>
    /// <returns>The details of the Alert</returns>
    public string getDetails()
    {
        return details;
    }
    /// <summary>
    /// Get the timestamp of the Alert
    /// </summary>
    /// <returns>The date and time of when the Alert was made</returns>
    public DateTime getTimeStamp()
    {
        return timeStamp;
    }
}
