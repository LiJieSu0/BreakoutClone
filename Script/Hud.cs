using Godot;
using System;
using Microsoft.Data.Sqlite;
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
		GetNode<Button>("GameOver/PlayAgainBtn").Pressed+=()=>{
			PackedScene scene=GD.Load<PackedScene>("res://Scene/MainScene.tscn");
			GetTree().ChangeSceneToPacked(scene);
		};
		GetNode<Button>("GameOver/QuitBtn").Pressed+=()=>{
			PackedScene scene=GD.Load<PackedScene>("res://Scene/MainMenu.tscn");
			GetTree().ChangeSceneToPacked(scene);
		};
	}

    private void AddRecord(){
		string name=_nameTextEdit.Text;
		string score=_scoreTextEdit.Text;
		if(name==""||score=="")
			return;
		connection.Open();
		SqliteCommand sqlCommand = connection.CreateCommand();
		sqlCommand.CommandText = 
			@"
				INSERT INTO player_score (name,score)
				VALUES (@name, @score)
			";
		sqlCommand.Parameters.AddWithValue("@name",name);
		sqlCommand.Parameters.AddWithValue("@score",score);
		try{
			sqlCommand.ExecuteNonQuery();
			GD.Print("Inserted value");
		}catch(Exception e){
			GD.Print("Something error when inserting");
		}finally{
			connection.Close();
		}
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
		GetNode<Control>("GameOver").Show();
	}

}
