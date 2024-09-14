using Godot;
using System;
using Microsoft.Data.Sqlite;
using Godot.Collections;
using System.Threading.Tasks;
public partial class Hud : CanvasLayer{
	#region Nodes
	TextEdit _nameTextEdit;
	TextEdit _scoreTextEdit;
	Button _submitBtn;
    private Panel _historyPanel;
    #endregion

    #region Variables
    SqliteConnection connection;
    string connectionString = "Data Source=ScoreBoard.db";
	#endregion
	public override void _Ready(){
		connection = new SqliteConnection(connectionString);
		InitializeNode();
		InitializeSignal();

	}

	public override void _Process(double delta){

	}
	
	private void InitializeNode(){
		_nameTextEdit=GetNode<TextEdit>("HistoryScore/NameTextEdit");
		_scoreTextEdit=GetNode<TextEdit>("HistoryScore/ScoreTextEdit");
		_submitBtn=GetNode<Button>("HistoryScore/SubmitBtn");
		_historyPanel=GetNode<Panel>("HistoryScore/HistoryPanel");
	}
	
	private void InitializeSignal(){
		_submitBtn.Pressed+=ShowRecord;
		GameManager.Instance.gameOverEvent+=ShowGameOverMenu;
		GetNode<Button>("GameOver/VBoxContainer/PlayAgainBtn").Pressed+=()=>{
			GetTree().ReloadCurrentScene();
		};
		GetNode<Button>("GameOver/VBoxContainer/QuitBtn").Pressed+=()=>{
			PackedScene scene=GD.Load<PackedScene>("res://Scene/MainMenu.tscn");
			GetTree().ChangeSceneToPacked(scene);
		};
	}



	private void ShowRecord(){
		connection.Open();
		SqliteCommand sqlCommand = connection.CreateCommand();
		sqlCommand.CommandText = 
			@"
				SELECT name, score
				FROM player_score
				ORDER BY score DESC
				LIMIT 10
			";
		try{
			using(var reader=sqlCommand.ExecuteReader()){
				while(reader.Read()){
					var name=reader.GetString(0);
					var score=reader.GetString(1);
					if(score=="0")
						continue;
					string tmp=name+" "+score;
					GD.Print(tmp);
					AddRecordToPanel(name,score);
				}
			}
		}catch(Exception e){
			GD.Print("Read err "+e);
		}finally{
			connection.Close();
		}
		_historyPanel.Show();
	}

	private void AddRecordToPanel(string name,string score){
		Label nameLabel=new Label();
		nameLabel.Text=name;
		Label scoreLabel=new Label();
		scoreLabel.Text=score;
		GetNode("HistoryScore/HistoryPanel/VBoxContainer/HBoxContainer/NameContainer").AddChild(nameLabel);
		GetNode("HistoryScore/HistoryPanel/VBoxContainer/HBoxContainer/ScoreContainer").AddChild(scoreLabel);
	}

	public void ShowGameOverMenu(){
		connection.Open();
		GetNode<Control>("GameOver").Show();
		int currScore=GetTree().CurrentScene.GetNode<ScoreLabel>("ScoreLabel").score;
		float time=GetTree().CurrentScene.GetNode<TimerLabel>("TimerLabel").time;
		int totalScore=(int)Mathf.Ceil(100-time)+currScore;
		GetNode<Label>("GameOver/VBoxContainer/TotalScoreLabel").Text="Score: "+totalScore.ToString();
		Button submitBtn=GetNode<Button>("GameOver/VBoxContainer/AddRecord/SubmitBtn");
		submitBtn.ButtonUp+=async ()=>{
			await AddRecord(totalScore);
		};
		connection.Open();
		SqliteCommand sqlCommand = connection.CreateCommand();
		Array<int> scoreArr=new Array<int>();
		sqlCommand.CommandText = 
			@"
				SELECT name, score
				FROM player_score
				ORDER BY score DESC
				LIMIT 5
			";
		try{
			using(var reader=sqlCommand.ExecuteReader()){
				while(reader.Read()){
					var name=reader.GetString(0);
					int score=int.Parse(reader.GetString(1));
					if(score==0)
						continue;
					scoreArr.Add(score);
				}
			}
		}catch(Exception e){
			GD.Print("Read err "+e);
			connection.Close();
		}finally{
			connection.Close();
			GD.Print("readClose");
		}

		foreach(var s in scoreArr){
			if(totalScore>=s){
				GetNode<Control>("GameOver/VBoxContainer/AddRecord").Show();
				break;
			}
		}
	}
    private async Task AddRecord(int score){
		connection.Open();
		GD.Print("add record Btn pressed");
		string name = GetNode<TextEdit>("GameOver/VBoxContainer/AddRecord/NameTextEdit").Text;
		if (string.IsNullOrEmpty(name))
			return;
		await Task.Run(() =>{
			SqliteCommand sqlCommand = connection.CreateCommand();
			sqlCommand.CommandText =
				@"
					INSERT INTO player_score (name,score)
					VALUES (@name, @score)
				";
			sqlCommand.Parameters.AddWithValue("@name", name);
			sqlCommand.Parameters.AddWithValue("@score", score);

			try
			{
				sqlCommand.ExecuteNonQuery();
				GD.Print("Inserted value");
			}
			catch (Exception e)
			{
				GD.Print("Something error when inserting: " + e.Message);
			}
			finally{
				connection.Close();
				GD.Print("Connection close");
			}
		});
    }
}
