<!DOCTYPE HTML>
<html>
	<head>
		<link href="style.css" rel="stylesheet"/>
		<meta charset="utf-8">
		<title>Таблица с меню (вариант 1)</title>
	</head>
	<body>
		<div>
			<table class="table">
				
				<tr>
					<?php
					$array=['Завтрак', 'Обед', 'Ужин'];
					foreach ($array as $arr)
					{
						echo '<th class="table th">$arr</th>';
					}
					?>
				</tr>
				<tr><td>Суп</td><td>Суп</td><td>Суп</td></tr>
				<tr><td>Горячее</td><td>Горячее</td><td>Горячее</td></tr>
				<tr><td>Холодное</td><td>Холодное</td><td>Холодное</td></tr>
			</table>
		</div>
	</body>
</html>