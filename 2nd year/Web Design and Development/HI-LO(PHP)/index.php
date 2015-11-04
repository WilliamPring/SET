<!--
File: index.php
Project : Web Assigment 3
Programmer : William Pring
First Version: 28 October 2015
Description: To create a HI LO game using PHP as one of the webserver technology
-->


<!DOCTYPE html>
<html>
<head>
	<title>Hi-Low Game</title>

	<script type="text/javascript">

	
	/*	
	Function Name: checkName
	return: bool
	Parameters: None
	Purpose: To check if name is a valid or not if not then error message will be
	Display
	*/

	function checkName()
	{
		var retStatus = true;
		var allSpans = document.getElementsByTagName("span");
			for (var i = 0; i < allSpans.length; i++)
		{
			allSpans[i].style.display = "inline-block";
			allSpans[i].style.visibility = "hidden";
		}
		var nameID = document.game.name;
		if(nameID.value == "")
		{
			allSpans[0].style.visibility = "visible";
			allSpans[0].innerHTML = "*Required!";
			retStatus = false;
		}
		else if(!/^[a-zA-Z]+$/.test(nameID.value))
		{
			allSpans[0].style.visibility = "visible";
			allSpans[0].innerHTML = "*Must only contain alphabets and no spaces!";
			retStatus = false;
		}	
		
		return retStatus;
	}
	/*	
	Function Name: checkMaxNumber
	return: bool
	Parameters: None
	Purpose: To check if interger user inputed max number is valid or not if not then error message will be
	Display
	*/
	function checkMaxNumber()
	{
		var retStatus = true; 
		var allSpans = document.getElementsByTagName("span");
		for (var i = 0; i < allSpans.length; i++)
		{
			allSpans[i].style.display = "inline-block";
			allSpans[i].style.visibility = "hidden";
		}
		var numberID = document.game.maxNumber;
		if(numberID.value == "")
		{
			allSpans[0].style.visibility = "visible";
			allSpans[0].innerHTML = "*Required!";
			retStatus = false;  
		}
		else if(!/^\d+$/.test(numberID.value))
		{
			allSpans[0].style.visibility = "visible";
			allSpans[0].innerHTML = "*Must only be a positive integer!";
			retStatus = false;
		}
		else if(numberID.value == 0 || numberID.value == 1)
		{
			allSpans[0].style.visibility = "visible";
			allSpans[0].innerHTML = "*Number must be greater than 1!";
			retStatus = false;
		}		
	
		return retStatus; 
	}
	/*	
	Function Name: checkMaxNumber
	return: bool
	Parameters: None
	Purpose: To check if user inputed is a valid interger or not if not then error message will be
	Display
	*/
	function checkUserInput()
	{
		var retStatus = true; 
		var allSpans = document.getElementsByTagName("span");
		for (var i = 0; i < allSpans.length; i++)
		{
			allSpans[i].style.display = "inline-block";
			allSpans[i].style.visibility = "hidden";
		}
		var numberID = document.game.userInput;
		if(numberID.value == "")
		{
			allSpans[0].style.visibility = "visible";
			allSpans[0].innerHTML = "*Required!";
			retStatus = false;  
		}
		else if(!/^\d+$/.test(numberID.value))
		{
			allSpans[0].style.visibility = "visible";
			allSpans[0].innerHTML = "*Must only be a positive integer!";
			retStatus = false;
		}
		else if(numberID.value == 0 || numberID.value == 1)
		{
			allSpans[0].style.visibility = "visible";
			allSpans[0].innerHTML = "*Number must be greater than 1!";
			retStatus = false;
		}		
	
		return retStatus; 
	}



	</script>
	<!--
	css embedded
	Purpose: Change styles for the html body
	-->

	<style type="text/css">
		body{
			background-color: white;
		}

		button:hover, input[type = "submit"]:hover, input[type="reset"]:hover, input[type="button"]:hover{
			background-color: lightblue;
		}

		button{
			width: 180px;
			margin: auto;
			position: absolute;
			top: 20%;
			bottom: 20%;
			left: 0;
			right: 0;
			border: 2px solid #f5f5f5;
			display: block;
			box-shadow: 0 0 3px grey;
			font-size: 20px;
		}
		h1, p {
			color:lightblue;
			font-family: "Arial", Times, serif;

		}

		.startingGame{
			display: none;
		}

		.error{
			color: #DC3D24;
			font-style: italic;
			font-size: 17px;
			visibility: hidden;
		}

		.container{
		/* text-align: center; */
		 background: #fff;
            	 margin: 50px auto;
            	 width: 400px;
            	 display: none;
		}
	</style>
<?php
?>
</head>

<body>  

		<h1>Let's play Hi-Low!</h1>
		<?php	
			$StateVar = 1;
			//check the request method
			//check to see if it a post or not
			if ($_SERVER["REQUEST_METHOD"] == "POST")
			{
				
				//set the name
				 $MyName = $_REQUEST['name'];
				 //set the max number
			     $MaxNumber = $_REQUEST['maxNumber'];
			     //set the min number
			     $MinNumber = $_REQUEST['minNumber'];
			     $UserChoice = $_REQUEST['userInput']; 
			     $SuperRandomNumber=  $_REQUEST['random']; 
				 //check to see if the name is empty if it is it will return false and also check MaxNumber to see if its null 
				 $StateMachine = $_REQUEST['state']; 
				 if (empty($StateMachine))
				{
				 	//set the state to 1
					 $StateVar = 1;
				 }
				 //check to see if name is not empty and check to seee if maxNumber is empty
				 else if ($StateMachine == 1)
				 {
				 	//set the state to 2
				 	$StateVar = 2;
				 }
				 //check to see if maxnumber has something inside then check if name is null and check if the min number is empty or not
				 else if($StateMachine == 2) 
				 {
				 	//set it to 3 the state
				 	$StateVar = 3;
				 	$RandomGeneratedNumber = rand($MinNumber, $MaxNumber);
				 	$SuperRandomNumber = $RandomGeneratedNumber;
				 } 
				 else if($StateMachine == 3)
				 {
				 	//set program set on 4
				 	$StateVar = 4;
				 }
				 else if ($StateMachine ==4)
				 {
				 	//set program set on 5
				 	$StateVar = 5;
				 }
			}
		?>
		
		<?php 
		//check to see if you have to put your name
		if ($StateVar == 1)
		{
		?>
			<p>
				<form method="post" name="game" onsubmit="return checkName()" action="<?php echo htmlspecialchars($_SERVER["PHP_SELF"]);?>"> 
				<label for="name">Enter your name: </label><br><br>
				<input type="text" name="name" placeholder="Enter name" maxlength="10" />
				<span class="error"></span>

				<!-- setting values back to it will remember its values -->
				<input type = "hidden" name="state" value = "1"/>
				<br>
				<!-- Button -->
				<input type="submit" value="Submit"/>
			</p>
		<?php 
		}
		
		?>

		<?php 
		//check to see if you have to put a max value
		if ($StateVar == 2)
		{
			 
		?>
			<p>
				<script type="text/javascript">
				<!-- change the back ground back to white -->
				document.body.style.backgroundColor = "white";
				</script>
				<form method="post" name="game" onsubmit="return checkMaxNumber()" action="<?php echo htmlspecialchars($_SERVER["PHP_SELF"]);?>"> 
				<label for="maxNumber"><?php echo $MyName ?> enter a MaxNumber:<br><br>
				<input type="text" name="maxNumber" placeholder="Enter positive integer" maxlength="10"/>
			    <span class="error"></span>
			  	<!-- setting values back to it will remember its values -->
			    <input type="hidden" name="name" value="<?php echo $MyName ?>" />
				<input type = "hidden" name="state" value = "2"/>
				<input type="hidden" name="minNumber" value="1"/>
								<br>
						<!-- Button -->
						<input type="submit" value="Submit"/>
			</p>
			<br>
		<?php 
		}
		?>

		<?php
		//check to see if user input betwen numbers 
		if ($StateVar == 3)
		{
		?>
		<p>
			<form method="post" name="game" onsubmit="return checkUserInput()" action="<?php echo htmlspecialchars($_SERVER["PHP_SELF"]);?>"> 
			<label for="userInput"><?php echo $MyName ?> Enter a number between <?php echo $MinNumber?> and <?php echo $MaxNumber?> </label><br><br>
			<input type="text" name="userInput" placeholder="Enter positive integer" maxlength="10"/>
			 <span class="error"></span>

			 <!-- setting values back to it will remember its values -->
			<input type="hidden" name="name" value="<?php echo $MyName ?>" />
			<input type ="hidden" name="state" value = "3"/>
			<input type="hidden" name="maxNumber" value="<?php echo $MaxNumber ?>"/>
			<input type="hidden" name="minNumber" value= "1"/>
			<input type="hidden" name="random" value="<?php echo $SuperRandomNumber?>"/>
			<br>
			<!-- Button -->
			<input type="submit" value="Submit"/>
		</p>
		<?php 

		}
		?>

		<?php 
		//check to see if numbers are valid or not
		if ($StateVar == 4)
		{
		?>
			<?php
			//check to see if userChoice is greater then the random number
			if($UserChoice < $SuperRandomNumber)
			{
				echo "that number you have enter is to small ";
				//check to see if userInput is grater then MinNumber
				if ($UserChoice >= $MinNumber)
				{
					//if so set it userinput
					$MinNumber = $UserChoice;

				}
				//display the range
				echo $MinNumber." to ".$MaxNumber;

			} 
			?>
			<?php
			if($UserChoice > $SuperRandomNumber)
			{
				echo "that number you have enter is to big";
			//check to see if userChoice is less then the random number
				if ($UserChoice <= $MaxNumber)
				{
					//if so set it userinput
					$MaxNumber = $UserChoice;
				}
				//display the range
				echo $MinNumber." to ".$MaxNumber;

			} 
			?>

			<form method="post" name="game" onsubmit="return checkUserInput()" action="<?php echo htmlspecialchars($_SERVER["PHP_SELF"]);?>"> 
			
			<p>
			<input type="text" name="userInput" placeholder="Enter a number" maxlength="10"/>
			</p>
			
			
			<?php 
			if ($UserChoice == $SuperRandomNumber)
			{
				//display message
				echo "YOU HAVE WON!!!!!!!";
			?>
			<br>

			<script type="text/javascript">
			//set the background to red if you win
				document.body.style.backgroundColor = "red";
			</script>
			<input type="hidden" name="state" value = "1"/>
			<input type="submit" value="Try Again"/>
			<?php
			}
			else
			{
			?>
				<input type="hidden" name="state" value = "3"/>
				<input type="submit" value="Submit"/>
			<?php
			}
			?>	

			<!-- setting values back to it will remember its values -->
			<input type="hidden" name="name" value="<?php echo $MyName ?>" />
			<input type="hidden" name="maxNumber" value="<?php echo $MaxNumber ?>"/>
			<input type="hidden" name="minNumber" value="<?php echo $MinNumber ?>"/>
			<input type="hidden" name="random" value="<?php echo $SuperRandomNumber?>"/>
							<br>


		<?php 
		}
		?>
		
		</form>

	<div class="container">
		<h1>This is the game</h1>
	</div>

</body>
</html>

