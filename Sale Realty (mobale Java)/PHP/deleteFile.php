
<?php

    include_once("dbConection.php");
    $result = $_POST['Query'];
     //$result = "SELECT  state,b.region AS region ,human_settlements,street,photo,id_realtys FROM realtys a, regions b WHERE a.id_region = b.id_region and id = 22 <> 23";
     list($update,$id) = preg_split('[<>]',$result); 
     //echo $id;
    $link = Conn();

    if (!$link) {
		die("Connection failed: " . mysqli_connect_error());
    }else
    {
        //Insert();
        //delete();
        //Insert($query);
        AllInfo($update,$id);
    }
    
  
    function AllInfo($quer,$id)
    {
        global $link;

        $result =mysqli_query($link,"DELETE FROM realtys WHERE id_realtys = ".$id);
        $result =mysqli_query($link,"DELETE FROM desires WHERE id_realtys = ".$id);
        $result =mysqli_query($link,"DELETE FROM rating_realtys WHERE id_realtys = ".$id);

        
        $resultt =mysqli_query($link,$quer);

        if(mysqli_num_rows($resultt)>0)
        {
            while($row=mysqli_fetch_array($resultt))
	        {
	            $flag[]=$row;
	        }
            print(json_encode($flag));
        }else
            echo "Ошибка";
    }
?>