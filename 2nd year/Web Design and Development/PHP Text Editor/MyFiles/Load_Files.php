<?php
	/*
	Filename: Load_Files.php
	Assignment: Assignment 5 WDD
	By: Naween Mehanmal
	Date: December 4, 2015
	Description: The server side script the handles the beginning read all of the files 
	from a directory function 
	*/


	header('Content-type: text/javascript');
	$dir = 'MyFiles';
	$fileList = glob("*.txt");

	$json = array('FileName' => $fileList);

	echo json_encode($json);		
?>