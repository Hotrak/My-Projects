<?php


include_once("dbConection.php");

$result = $_POST['Query'];
//$result = "INSERT INTO users (name,phone,email,password,coin,creation_date) VALUES ('kOK','293956378','vladtumen2000@mail.ru','123','0','<@>')<>123";
$link = Conn();

if (!$link) {
    die("Connection failed: " . mysqli_connect_error());
}else
{
    $data = date("Y-m-d H:i:s");
     //= "INSERT INTO users (name,phone,email,password,coin,creation_date) VALUES ('Vlad','+3752939578','vladtumen2200@mail.ru','123','0','<@>')<>123";


    list($qyer,$pass) = preg_split('[<>]',$result); 
    $qyer = str_replace('<@>',$data, $qyer);

    $heshPas = password_hash($pass,PASSWORD_DEFAULT);

    $qyer = str_replace($pass,$heshPas, $qyer);
    echo $qyer;

    Insert($qyer);
}
function Insert($query)
    {
        global $link;
        $result =mysqli_query($link,$query);

        if($result)
        {
            $mess["error"] = "1";
            $mess["kod"] = "123";

        }else
            $mess["error"] = "0";
            
            $mess["mess"] = $result;
        echo json_encode($mess);

    }





?>