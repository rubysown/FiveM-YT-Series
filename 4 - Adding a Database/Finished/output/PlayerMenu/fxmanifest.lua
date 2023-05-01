version '1.0.0'

fx_version 'cerulean'
games { 'gta5' }

client_script {
	'PlayerMenu.Client.net.dll'
}

server_script {
	'PlayerMenu.Server.net.dll'
}

server_files {
	'Dapper.dll',
	'MySql.Data.dll'
}
