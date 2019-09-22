<?php
    //require_once'dbConection.php';
    //$link = mysqli_connect(DB_HOST,DB_USERNAME,DB_PASWORD,DB_NAME) or die("Ошибка " . mysqli_error($link));
    $ID = $_POST['ID'];
    //$ID = 1;

    $message[Surname] = "Palal";
    $message[Name] = "$ID";
    $message[Middlename] = "Jora";
    
    echo (json_encode($message));
?>