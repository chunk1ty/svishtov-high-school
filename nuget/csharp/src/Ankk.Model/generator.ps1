 # .\protoc.exe protoc -I=./protos --csharp_out=./generated ./protos/test.proto

function RunCodeGeneration($tool, $arguments)
{
	$pinfo = New-Object System.Diagnostics.ProcessStartInfo
	$pinfo.FileName = $tool
	$pinfo.Arguments = $arguments
	$pinfo.UseShellExecute = $false
	$pinfo.CreateNoWindow = $true
	$pinfo.RedirectStandardOutput = $true
	$pinfo.RedirectStandardError = $true
	$pinfo.WorkingDirectory = $PSScriptRoot
	
	$process = New-Object System.Diagnostics.Process
	$process.StartInfo = $pinfo
	
	$process.Start() | Out-Null
	   
	$stdout = $process.StandardOutput.ReadToEnd()
	$stderr = $process.StandardError.ReadToEnd()
	
	if($stderr)
	{
	    Write-Output $stderr
	    exit 1
	}
	
	Write-Output $stdout
}

$protoc = "$($env:USERPROFILE)\.nuget\packages\google.protobuf.tools\3.6.1\tools\windows_x64\protoc.exe"

if(-not (Test-Path $protoc))
{
	Write-Output "$protoc not found."
	Exit 1
}

$outFolder = 'generated'
If(-not (test-path $outFolder))
{
	New-Item -ItemType Directory -Force -Path $outFolder
}

$rootFolder = $PSScriptRoot | split-path | split-path | split-path 
$protosFolder = "$($rootFolder)\protos"

$args = "-I=$($protosFolder) --csharp_out=.\$($outFolder) --proto_path= $($protosFolder)\course.proto"

RunCodeGeneration $protoc $args
Write-Output 'course.proto'