<?php
if($_SERVER['REQUEST_METHOD'] == 'GET'){
    $date = date('Y');
    echo json_encode("
    <footer>
        <p>&copy; $date Autóműhely</p>
    </footer>");
}