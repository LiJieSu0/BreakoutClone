using Godot;
using System;
using Microsoft.Data.Sqlite;

public partial class SqliteTest : Node2D
{
	Button _insertBtn;
	Button _createTableBtn;
	TextEdit _nameTextEdit;
	TextEdit _scoreTextEdit;
	TextEdit _resultTextEdit;
    private Button _searchBtn;
    string connectionString = "Data Source=ScoreBoard.db";
	SqliteConnection connection;

	public override void _Ready(){
		connection = new SqliteConnection(connectionString);
		InitializeNode();
		InitializeSignal();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void CreateTable(){
		connection.Open();
		SqliteCommand createTableCmd = connection.CreateCommand();
        createTableCmd.CommandText = 
            @"
                CREATE TABLE IF NOT EXISTS player_score (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    name TEXT NOT NULL,
                    score INTEGER NOT NULL
                );
            ";
		createTableCmd.ExecuteNonQuery();
		GD.Print("Table Created");
	}

	public void InsertData(){
		if(_nameTextEdit.Text==""||_scoreTextEdit.Text==""){
			GD.Print("Insert failed");
			_nameTextEdit.Text="";
			_scoreTextEdit.Text="";
			return;
		}
		string name=_nameTextEdit.Text;
		int score=_scoreTextEdit.Text.ToInt();

		connection.Open();
		SqliteCommand insertCommand=connection.CreateCommand();
		insertCommand.CommandText=@"
			INSERT INTO player_score (name,score)
			VALUES (@name,@score)
		";
		insertCommand.Parameters.AddWithValue("@name",name);
		insertCommand.Parameters.AddWithValue("@score",score);
		try{
            // Execute the command
            insertCommand.ExecuteNonQuery();
            GD.Print("Inserted 1 value");
			_nameTextEdit.Text="";
			_scoreTextEdit.Text="";
        }catch (Exception ex){
            GD.PrintErr($"Error inserting data: {ex.Message}");
        }finally{
            connection.Close();
        }
	}

	private void SearchByScore(){
		int scoreThreshold=_scoreTextEdit.Text.ToInt();
		connection.Open();
        SqliteCommand selectCommand = connection.CreateCommand();
		selectCommand.CommandText=@"
			SELECT * FROM player_score
			WHERE score >@scoreThreshold
		";
		selectCommand.Parameters.AddWithValue("@scoreThreshold",scoreThreshold);
		try{
			using(var reader=selectCommand.ExecuteReader()){
				while(reader.Read()){
					var id = reader.GetInt32(0);
                    var name = reader.GetString(1);
                    var score = reader.GetInt32(2);
					string tmpRes=id.ToString()+" "+name.ToString()+" "+score.ToString()+"\n";
					_resultTextEdit.Text+=tmpRes;
				}

			}
		}catch(Exception e){
			GD.Print("Search error :"+e);
		}finally{
			connection.Close();
		}

	}

	private void InitializeNode(){
		_insertBtn=GetNode<Button>("InsertBtn");
		_createTableBtn=GetNode<Button>("CreateTableBtn");
		_nameTextEdit=GetNode<TextEdit>("NameLabel/NameTextEdit");
		_scoreTextEdit=GetNode<TextEdit>("ScoreLabel/ScoreTextEdit");
		_resultTextEdit=GetNode<TextEdit>("SearchResult");
		_searchBtn=GetNode<Button>("SearchBtn");
	}

	private void InitializeSignal(){
		_insertBtn.Pressed+=InsertData;
		_createTableBtn.Pressed+=CreateTable;
		_searchBtn.Pressed+=SearchByScore;
	}



}
