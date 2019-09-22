<?php
    require_once'dbConection.php';
    $uoload_path= 'testImg/';
    $server_ip = gethostbyname(gethostname());
    //$upload_url = 'http://' .$server_ip. '/img'.'/'. $uoload_path;
    $upload_url = 'http://' .$server_ip. '/testImg';
    
    $response = array();

    if($_SERVER['REQUERST_METHOD']=='POST')
    { 
        if(isaset($_POST['name']) and isset($_FILES['image']['name']))
        {
            $con =mysqli_connect(DB_HOST,DB_USERNAME,DB_PASWORD,DB_NAME) or die('unable to connect to database');

            $name = $_POST['name'];
            $fileinfo = pathinfo($_FILES['image']['name']);
            $extension = $fileinfo['extension'];
            $file_url = $upload_url . getFileName() . '.' . $extension;
            $file_path = $uoload_path . getFileName(). '.' . $extension;

            try
            {
                move_upload_file($_FILES['image']['tmp_image'],$file_path);

                $sql = "INSERT INTO images (url,name) VALUES('$file_url','$name')";
                if(mysqli_query($con,$sql))
                {
                    $response['error'] = false;
                    $response['url'] =$file_url;
                    $response['name'] =$name;                   
                }
            }
            catch(Exception $e)
            {
                $response['error'] = false;
                $response['message'] = $e ->getMessage();
            }
            
        }else {
            $response['error'] = true;
            $response['message'] = 'plase choose some file';

        }
        echo json_encode($response);
    }else
        echo 'error';
    function getFileName()
    {
        // $con = mysqli_connect(DB_HOST,DB_USERNAME,DB_PASWORD,DB_NAME) or die('Unable to connect');

        // $sql = 'SELECT max(id) as id FROM images';

        // $result = mysqli_fetch_array(mysqli_query($con,$sql));
        // mysqli_close($con);

        $getMime = explode('.', $file['name']); 
        // нас интересует последний элемент массива - расширение 
        $mime = strtolower(end($getMime)); 
        $file_name = mt_rand(0,1000000) . "( ". $today . " )" . mt_rand(0,1000000) . mt_rand(0,1000000) . mt_rand(0,1000000); 
        return file_name;
        // if($result['id']==null)
        //     return 1;
        // else
        //     return ++$result['id'];
    }
?>
