<?php
   define('DB_HOST','localhost');
   define('DB_USERNAME','malaha_mtk');
   define('DB_PASWORD','3&gE4His');
   define('DB_NAME','malaha_mtk');
function Conn()
{
  $con =mysqli_connect(DB_HOST,DB_USERNAME,DB_PASWORD,DB_NAME);
  return $con;
}
?>
  

