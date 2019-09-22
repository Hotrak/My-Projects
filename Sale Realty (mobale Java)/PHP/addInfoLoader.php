<?php

    include_once("dbConection.php");

    //$quer = $_POST['Query'];
    $link = Conn();

    if (!$link) {
		die("Connection failed: " . mysqli_connect_error());
    }else
    {
        $areas = loadData("SELECT * FROM areas");
        $durations = loadData("SELECT * FROM durations");
        $regions = loadData("SELECT * FROM regions");
        $type_realtys = loadData("SELECT * FROM type_realtys");
        $type_houses = loadData("SELECT * FROM type_houses");
        $type_upkeeps = loadData("SELECT * FROM type_upkeeps");
        $type_layouts = loadData("SELECT * FROM type_layouts");
        // $areas = loadData("SELECT * FROM area");
        // $areas = loadData("SELECT * FROM area");
        // $areas = loadData("SELECT * FROM area");
        


        $arr = array('areas' => json_encode($areas),
        'durations' => json_encode($durations),
        'regions' => json_encode($regions),
        'type_realtys' => json_encode($type_realtys),
        'type_houses' => json_encode($type_houses),
        'type_upkeeps' => json_encode($type_upkeeps),
        'type_layouts' => json_encode($type_layouts)
    );

    
        // $flag[] = json_encode($areas);
        // $flag[] = json_encode($durations);


        print(json_encode($arr));
    }

    function loadData($quer)
    {
        global $link;

        $result =mysqli_query($link,$quer);

        if(mysqli_num_rows($result)>0)
        {
            while($row=mysqli_fetch_array($result))
	        {
	            $flag[]=$row;
	        }
            return $flag;
        }else
            return -1;
    }
?>