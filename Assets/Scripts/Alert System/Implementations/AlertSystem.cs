using UnityEngine;

public class AlertSystem : MonoBehaviour {

    Queue alertQueue;

	/// <summary>
    /// Creates a new Queue on project startup
    /// </summary>
	void Start () {
        alertQueue = new Queue();
	}

    /// <summary>
    /// Adds a new Alert to the Queue
    /// </summary>
    /// <param name="alert">The alert to add to the Queue</param>
    public void addAlert(Alert alert)
    {
        alertQueue.push(alert);
    }

    /// <summary>
    /// Removes the oldest Alert from the Queue
    /// </summary>
    /// <returns>The oldest Alert</returns>
    public Alert getAlert()
    {
        return alertQueue.pop();
    }
}
