using UnityEngine;
using System.Collections;
using Amazon;
using Amazon.CognitoIdentity;
using Amazon.Runtime;
using System.Collections.Generic;
using UnityEngine.UI;
using Amazon.SQS;
using Amazon.SQS.Model;
using System.IO;

public class sqsHandler : MonoBehaviour {

	public class Play
	{
		public string sourcePlayerName { get; set; }
		public string targetPlayerName { get ; set; }
		public string bugType { get; set; }
	}

	public GameManagerBehavior manager; 

	void Start() {
		manager = GameObject.Find ("GameManager").GetComponent<GameManagerBehavior> ();
	}

	private string IDENTITY_POOL_ID, AWS_KEY, AWS_SECRET;

	string[] lines = System.IO.File.ReadAllLines("./.git/auth.conf");
	public Text resultText;
	AmazonSQSClient sqsClient;

	// Use this for initialization
	void Awake () {
		IDENTITY_POOL_ID = lines[0];
		AWS_KEY = lines[1];
		AWS_SECRET = lines[2];

		UnityInitializer.AttachToGameObject (this.gameObject);
		sqsClient = new AmazonSQSClient(AWS_KEY, AWS_SECRET, RegionEndpoint.USEast1);


	}

	public void PerformPlayStore (string bType, string sourceName)
	{
		string targetPlayerName = sourceName;
		sqsClient.GetQueueUrlAsync("UpNext", (qURLReqCallback)=>{
			if(qURLReqCallback == null){
				print("QUEUE DOES NOT EXIST");
				targetPlayerName = sourceName;
			}else{
				sqsClient.ReceiveMessageAsync("UpNext", (response)=>{
					ReceiveMessageResponse resp = response.Response;
					targetPlayerName = resp.Messages[0].ToString();
				});
			}
		});


		Play myPlay = new Play {
			sourcePlayerName = sourceName + manager.playerUID,
			targetPlayerName = targetPlayerName,
			bugType = bType,
		};

		sqsClient.CreateQueueAsync(myPlay.targetPlayerName + "_bugs", (result) => {
			if(result  == null){
				print("QUEUE COULD NOT BE CREATED");
				throw(new QueueDoesNotExistException("Queue " + myPlay.targetPlayerName + "_bugs could not be created. We give up!"));	
			}
		});

		sqsClient.GetQueueUrlAsync(myPlay.targetPlayerName+"_bugs", (qURLReqCallback)=>{
			if(qURLReqCallback == null){
				print("QUEUE DOES NOT EXIST");
			}else{
				string message = "{'sourcePlayerId' : '" + myPlay.sourcePlayerName + "','targetPlayerId' : '" + myPlay.targetPlayerName + "','bugType' : '" + myPlay.bugType + "'}"; 
				SendMessageRequest request = new SendMessageRequest(qURLReqCallback.Response.QueueUrl, message);
				sqsClient.SendMessageAsync(request, (response)=>{
					if(response == null)
						throw(new InvalidMessageContentsException("The following message could not be enqueued: " + message));
				});
			}
		});

	}

	public void UpNext(string playerId){
		sqsClient.CreateQueueAsync("UpNext", (result) => {
			if(result  == null){
				Debug.Log("QUEUE COULD NOT BE CREATED");
				//throw(new QueueDoesNotExistException("UpNext queue either exists already or we could not create it, whe have given up on creating it!"));	
			}
		});

	
		sqsClient.GetQueueUrlAsync("UpNext", (qURLReqCallback)=>{
			if(qURLReqCallback == null){
				throw(new InvalidMessageContentsException("The queue does not exist and could not be created"));				
			}else{
				string message = manager.playerName + manager.playerUID; 
				SendMessageRequest request = new SendMessageRequest(qURLReqCallback.Response.QueueUrl, message);
				sqsClient.SendMessageAsync(request, (response)=>{
					if(response == null)
						//throw(new InvalidMessageContentsException("The following message could not be enqueued: " + message));
						Debug.Log("Message could not be enqueued: " + message);
				});
			}
		});
	}

	// Update is called once per frame
	void Update () {

	}
}
