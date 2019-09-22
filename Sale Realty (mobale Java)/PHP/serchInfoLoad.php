<?php

    include_once("dbConection.php");

    //$quer = $_POST['Query'];
    $link = Conn();

    if (!$link) {
		die("Connection failed: " . mysqli_connect_error());
    }else
    {
        $areas = loadData("SELECT areas.id_area, areas.area FROM areas INNER JOIN realtys ON areas.id_area = realtys.id_area  WHERE realtys.state = '2' GROUP BY areas.id_area, areas.area");
        $durations = loadData("SELECT durations.duration, durations.id_duration FROM areas, type_realtys, type_layouts, durations INNER JOIN realtys ON durations.id_duration = realtys.id_duration WHERE realtys.state = '2' GROUP BY durations.duration, durations.id_duration");
        $regions = loadData("SELECT regions.id_region, regions.region,regions.id_area FROM regions INNER JOIN realtys ON regions.id_region = realtys.id_region WHERE realtys.state = '2' GROUP BY regions.id_region, regions.region, regions.id_area" );
        $type_realtys = loadData("SELECT type_realtys.type_realty, type_realtys.id_type_realty FROM type_realtys INNER JOIN realtys ON type_realtys.id_type_realty = realtys.id_type_realty WHERE realtys.state = '2'  GROUP BY type_realtys.type_realty, type_realtys.id_type_realty");
        $type_houses = loadData("SELECT type_houses.id_type_house, type_houses.type_house FROM type_houses INNER JOIN realtys ON type_houses.id_type_house = realtys.id_type_house WHERE realtys.state = '2'  GROUP BY type_houses.id_type_house, type_houses.type_house");
        $type_upkeeps = loadData("SELECT type_upkeeps.id_type_upkeep, type_upkeeps.type_upkeep FROM type_upkeeps INNER JOIN realtys ON type_upkeeps.id_type_upkeep = realtys.id_type_upkeep WHERE realtys.state = '2'  GROUP BY type_upkeeps.id_type_upkeep, type_upkeeps.type_upkeep");
        $type_layouts = loadData("SELECT type_layouts.id_type_layout, type_layouts.type_layout FROM areas, type_realtys, type_layouts INNER JOIN realtys ON type_layouts.id_type_layout = realtys.id_type_layout WHERE realtys.state = '2' GROUP BY type_layouts.id_type_layout, type_layouts.type_layout");
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