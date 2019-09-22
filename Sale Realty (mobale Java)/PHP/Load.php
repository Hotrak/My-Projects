
<?php
//Status
//1- Админ не проверил
//2- Готово к аренде
//3- В данный момент не снимается 
    include_once("dbConection.php");

    $quer = $_POST['Query'];
    $link = Conn();

    if (!$link) {
		die("Connection failed: " . mysqli_connect_error());
    }else
    {
        //Insert();
        //delete();
        //Insert($query);
        AllInfo($quer);
    }
    
    function AllInfo($quer)
    {
        global $link;
        
        $result =mysqli_query($link,$quer);

        if(mysqli_num_rows($result)>0)
        {
            while($row=mysqli_fetch_array($result))
	        {
	            $flag[]=$row;
	        }
            print(json_encode($flag));
        }else
            echo "Ошибка";
    }
    function Insert($query)
    {
        global $link;
        
        $result =mysqli_query($link,$query);

        if($result)
        {
            $mess["error"] = "1";
        }else
            $mess["error"] = "0";
            
            $mess["mess"] = "Gotovo";
        echo json_encode($mess);

    }
    function Insert1()   
    {
        global $link;
        mysqli_query($link,"INSERT INTO TEST_vlad (fam,name,photo) VALUES ('Тумановский','Влад','http://malaha.beget.tech/img/1556776325899.JPG')");
        echo 'Insert secsesafule';
    }
    function delete()   
    {
        global $link;
        mysqli_query($link,"DELETE FROM TEST_vlad");
    }
?>