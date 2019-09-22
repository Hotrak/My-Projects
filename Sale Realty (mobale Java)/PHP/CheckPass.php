<?php

    include_once("dbConection.php");

     $quer = $_POST['Query'];
    // $quer = "SELECT `password`,`id` FROM users WHERE `email` = 'vladtumen2000@mail.ru' AND `id` = '22'";

    //$quer = "vladtumen2000@mail.ru<>123";
    // $login = "vladtumen2000@mail.ru";
    // $pass = "123";
    $link = Conn();

    if (!$link) {
		die("Connection failed: " . mysqli_connect_error());
    }else
    {
        
        Insert($quer);
    }
//SELECT `email`,`password`,`id`,`kod` FROM users WHERE `email` = '"+login.getText()+"' AND `password` = '123'
    function Insert($query)
    {
        
        global $link;
        
        $result =mysqli_query($link,$query);

        if(mysqli_num_rows($result)>0)
        {
            $row = mysqli_fetch_assoc($result); 
            $mess["error"] = "1";
            $mess["id"] = $row["id"];
            
                
        }else
            $mess["error"] = "0";

            $mess["mess"] = $result;
        echo json_encode($mess);
                    
            

    }
?>