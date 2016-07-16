using UnityEngine;
using System.Collections;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.CognitoIdentity;
using Amazon.Runtime;
using Amazon.DynamoDBv2.DataModel;
using System.Collections.Generic;
using UnityEngine.UI;
using Amazon.SQS;
using Amazon.SQS.Model;

public class ddbHandler : MonoBehaviour {
	/*
	[DynamoDBTable("Plays")]
	public class Play
	{
		[DynamoDBHashKey]   // Hash key.
		public int bugTime { get; set; }
		[DynamoDBProperty]
		public int sourcePlayerId { get; set; }
		[DynamoDBProperty]
		public int targetPlayerId { get; set; }
		[DynamoDBProperty]
		public string bugType { get; set; }
	}


	[DynamoDBTable("Bookstore")]
	public class Book
	{
		[DynamoDBHashKey]   // Hash key.
		public int id { get; set; }
		[DynamoDBProperty]
		public string Title { get; set; }
		[DynamoDBProperty]
		public string ISBN { get; set; }
		[DynamoDBProperty("Authors")]    // Multi-valued (set type) attribute.
		public List<string> BookAuthors { get; set; }
	}
*/
	public class Play
	{
		public int sourcePlayerId { get; set; }
		public int targetPlayerId { get ; set; }
		public string bugType { get; set; }

	}

	public DynamoDBContext ddbContext;
	public AmazonDynamoDBClient ddbClient;
	public Text resultText;
	private const string IDENTITY_POOL_ID = "arn:aws:iam::798924599061:user/mobileService";
	AmazonSQSClient sqsClient;

	// Use this for initialization
	void Awake () {
		
		UnityInitializer.AttachToGameObject (this.gameObject);
		//var credentials = new CognitoAWSCredentials("arn:aws:iam::798924599061:user/mobileService", RegionEndpoint.USWest2);
		//ddbClient = new AmazonDynamoDBClient (credentials);
		//ddbContext = new DynamoDBContext (ddbClient);
		//BasicAWSCredentials awsCredentials = new BasicAWSCredentials({},{});


		AmazonDynamoDBClient client = new AmazonDynamoDBClient("AKIAIUVCFSG7K244THMQ", "O1pUGHWMkhwyEIx4poAlDXzpm0dssePEH2TUu7Fy", RegionEndpoint.USEast1);
		sqsClient = new AmazonSQSClient("AKIAIUVCFSG7K244THMQ", "O1pUGHWMkhwyEIx4poAlDXzpm0dssePEH2TUu7Fy", RegionEndpoint.USEast1);

		//ddbContext = new DynamoDBContext(client);

	}

	public void PerformPlayStore(string bType) 
	{

		Play myPlay = new Play {

			sourcePlayerId = 1,
			targetPlayerId = 1,
			bugType = bType,
		};

		sqsClient.CreateQueueAsync(myPlay.targetPlayerId + "_bugs", (result) => {
			if(result  == null){
				print("QUEUE COULD NOT BE CREATED");
				throw(new QueueDoesNotExistException("Queue " + myPlay.targetPlayerId + "_bugs could not be created. We give up!"));	
			}
		});

		sqsClient.GetQueueUrlAsync(myPlay.targetPlayerId+"_bugs", (qURLReqCallback)=>{
			if(qURLReqCallback == null){
				print("QUEUE DOES NOT EXIST");
			}else{
				string message = "{'sourcePlayerId' : '" + myPlay.sourcePlayerId + "','targetPlayerId' : '" + myPlay.targetPlayerId + "','bugType' : '" + myPlay.bugType + "'}"; 
				SendMessageRequest request = new SendMessageRequest(qURLReqCallback.Response.QueueUrl, message);
				sqsClient.SendMessageAsync(request, (response)=>{
					if(response == null)
						throw(new InvalidMessageContentsException("The following message could not be enqueued: " + message));
				});
			}
		});
		
	}


	// Update is called once per frame
	void Update () {
	
	}
}
