<?php
	$text1 = $_POST["name"];
	$text2 = $_POST["kills"];

	if($text1 != ""){
		echo("Message sent!");
		echo("Field 1 =" . $text1);
		echo("Field 2 =" . $text2);
		$file = fopen("data.txt","c");
		fwrite($file,$text1);
		fwrite($file,$text2);
		fclose($file);
	}else{
		$file = fopen("data.txt","r");
		echo fread($file, filesize("data.txt"));
		fclose($file);
	}
?>