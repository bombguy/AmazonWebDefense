using UnityEngine;
using System.Collections;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.CognitoIdentity;
using Amazon.Runtime;
using Amazon.DynamoDBv2.DataModel;
using System.Collections.Generic;
using UnityEngine.UI;

public class ddbHandler : MonoBehaviour {

	[DynamoDBTable("Plays")]
	public class Play
	{
		[DynamoDBHashKey]   // Hash key.
		public int gameId { get; set; }
		[DynamoDBProperty]
		public int sourcePlayerId { get; set; }
		[DynamoDBProperty]
		public int targetPlayerId { get; set; }
		[DynamoDBProperty]
		public string bugType { get; set; }
	}

	private DynamoDBContext ddbContext;
	public Text resultText;

	// Use this for initialization
	void Awake () {
		RegionEndpoint usEast = RegionEndpoint.USEast1;
		CognitoAWSCredentials credentials = new CognitoAWSCredentials("arn:aws:iam::798924599061:user/mobileService", RegionEndpoint.USEast1);
		AmazonDynamoDBClient ddbClient = new AmazonDynamoDBClient(credentials,RegionEndpoint.USEast1);
		ddbContext = new DynamoDBContext (ddbClient);
	}

	public void PerformPlayStore()
	{
		Play play = new Play
		{
			gameId = 12345,
			sourcePlayerId = 00001,
			targetPlayerId = 00002,
			bugType = "testBug",
		};

		// Save the book.
		ddbContext.SaveAsync(play,(result)=>{
			if(result.Exception == null)
				print("play saved");
		});
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
