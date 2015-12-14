<?php
	/*
	Filename: Write_File.php
	Assignment: Assignment 5 WDD
	By: Naween Mehanmal
	Date: December 4, 2015
	Description: The server side script the handles all of the file io calls, opening, reading/writing 
	and saving files on the MyFiles directory located on the server side 
	*/

	header('Content-type: text/javascript');

	//Branch conditions checking to see which values were posted to run, either open, save old and or save a new file name
	
	
	if(isset($_POST["FileName"]))
	{		
		$FileName = $_POST["FileName"]; //The file name the user wants to open for viewing/editing 
		$fileInfo = file_get_contents("MyFiles\\" . $FileName);

		//Object specifying the status of the operation and the content found within the file that wanted to be opened by the user
		$msgBack = array('FileContent' => $fileInfo, 
						 'Status' => "Success in opening file!");

		echo json_encode($msgBack); //Encode as json object and send back to the client 
	}
	elseif(isset($_POST["SaveFileName"]))
	{		
		$newSaveFileName = $_POST["SaveFileName"]; //Name of the file that wants content to be saved on 
		$newContent = $_POST["Content"]; //The content that must be stored on the file name that is being saved 

		$file_path = "MyFiles\\" . $newSaveFileName; //Specific file name along with the relative path to the current file being executed

		$handle =  fopen($file_path, "w");

		if(is_writable($file_path))
		{
			$newContent = str_replace("\n", "\r\n", $newContent); //Have content in the same format as the client-sides
			//Text format 
			fwrite($handle, $newContent); //Append the info to the file being saved to
			fclose($handle); //Close handle, close file gracefully 
		} 

		$saveMsgBack = array('Status' => "Success in saving file!");
		echo json_encode($saveMsgBack);
	}
	elseif(isset($_POST["SaveAsFileName"]))
	{
		$newSaveAsFileName = $_POST["SaveAsFileName"];
		$newSaveAsContent = $_POST["SaveAsContent"];

		$file_path = "MyFiles\\" . $newSaveAsFileName;

		$handle =  fopen($file_path, "w"); //Open the file with the pointer at the beginning, write permissions being handled

		if(is_writable($file_path))
		{
			$newSaveAsContent = str_replace("\n", "\r\n", $newSaveAsContent); //Have content in the same format as the client-sides
			//Text format 
			fwrite($handle, $newSaveAsContent); //Append the info to the file being saved to
			fclose($handle);
		} 

		$saveMsgBack = array('Status' => "Success in saving new file name!");
		echo json_encode($saveMsgBack); //Parse and encode information as json object to send to the client-side
		//The client side is expecting the data returned to be in json format
	} 

	

?>