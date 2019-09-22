<?php

    include_once("dbConection.php");

    $quer = $_POST['Query'];
    $link = Conn();

    if (!$link) {
		die("Connection failed: " . mysqli_connect_error());
    }else
    {
        //Insert();
        //delete();
        Insert($quer);
    }

    function Insert($query)
    {
        global $link;
        
        $result =mysqli_query($link,$query);

        if(mysqli_num_rows($result)>0)
        {
            $mess["error"] = "1";
            $mess["kod"] = "123";

        }else
            $mess["error"] = "0";
            
            $mess["mess"] = $result;
        echo json_encode($mess);

    }
?>