using System.Collections.Generic;
using Clients;
using UnityEngine;

public class QueueManager : Singleton<QueueManager>
{
	public List<Transform> positionsDeLaFile; 
	private List<ClientController> clientsEnAttente = new List<ClientController>();

	public void JoinTheQueue(ClientController nouveauClient) 
	{
		if (!clientsEnAttente.Contains(nouveauClient))
		{
			clientsEnAttente.Add(nouveauClient);
			UpdateTheQueue();
		}
	}

	public void NextClient() 
	{
		if (clientsEnAttente.Count > 0) 
		{
			ClientController premier = clientsEnAttente[0];
			clientsEnAttente.RemoveAt(0);
            
			UpdateTheQueue();
		}
	}

	private void UpdateTheQueue() 
	{
		for (int i = 0; i < clientsEnAttente.Count; i++) 
		{
		
			if (i < positionsDeLaFile.Count) 
			{
				clientsEnAttente[i].Movement.AllerA(positionsDeLaFile[i].position);
				Debug.Log(positionsDeLaFile[i].position);
			}
		}
	}

	public void LeaveTheQueue(ClientController client)
	{
		if (clientsEnAttente.Contains(client))
		{
			clientsEnAttente.Remove(client);
			UpdateTheQueue();
		}
	}
}