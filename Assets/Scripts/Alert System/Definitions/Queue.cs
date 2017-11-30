using System.Collections.Generic;

public class Queue {

    List<Alert> alertList;

    /// <summary>
    /// Creates the Queue
    /// </summary>
    public Queue()
    {
        alertList = new List<Alert>();
    }
    
    /// <summary>
    /// Puts a new alert in the Queue
    /// </summary>
    /// <param name="newAlert">An alert to be added to the Queue</param>
    public void push(Alert newAlert)
    {
        alertList.Add(newAlert);
    }

    /// <summary>
    /// Returns the oldest Alert in the Queue
    /// </summary>
    /// <returns>The oldest Alert, or null if no alerts are in the Queue</returns>
    public Alert pop()
    {
        if(alertList.Count > 0)
        {
            Alert poppedAlert = new Alert(alertList[0]);  // Deep copy the alert to be removed
            alertList.RemoveAt(0);  // remove the alert at the top of the queue
            return poppedAlert;  // return the copied & popped alert
        }
        else
        {
            return null;  // return null if there are no items in the queue
        }
    }
}
