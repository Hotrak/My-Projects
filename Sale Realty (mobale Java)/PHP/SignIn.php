<?php

    include_once("dbConection.php");

    $quer = $_POST['Query'];
    //$quer = "vladtumen2000@mail.ru<>123";
    list($login,$pass) = preg_split('[<>]',$quer);
    // $login = "vladtumen2000@mail.ru";
    // $pass = "123";
    $link = Conn();

    if (!$link) {
		die("Connection failed: " . mysqli_connect_error());
    }else
    {
        
        Insert($login,$pass);
    }
//SELECT `email`,`password`,`id`,`kod` FROM users WHERE `email` = '"+login.getText()+"' AND `password` = '123'
    function Insert($login,$pass)
    {
        
        global $link;
        
        $result =mysqli_query($link,"SELECT `password`,`id` FROM `users` WHERE `email` = '$login'");

        if(mysqli_num_rows($result)>0)
        {
            $row = mysqli_fetch_assoc($result); 

            if (password_verify($pass, $row['password'])) {
                {

                    $mess["error"] = "1";
                    $mess["id"] = $row['id'];
                }
            }else
                $mess["error"] = "0";
                
        }else
            $mess["error"] = "0";

            $mess["mess"] = $result;
        echo json_encode($mess);
           


        // if(mysqli_num_rows($result)>0)
        // {
        //     $mess["error"] = "1";
        
       
        //     $row = mysqli_fetch_assoc($result); 
        //     //echo $row['id'];
          
            
           
        //     $mess["id"]= $row["id"];
        //     $mess["kod"] = $row["kod"];

        // }else
        //     $mess["error"] = "0";
            
            

    }
?>