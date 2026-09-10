#!/bin/bash

# MelissaMatchupObjectGlobalLinuxDotnet
#
# Downloads the required components and then builds and runs MelissaMatchupObjectGlobalLinuxDotnet.
#
# This script uses the Melissa Updater to fetch the data file(s), the shared object(s), and the
# C# wrapper, verifies the shared object(s) downloaded, then builds the .NET project and runs it
# against the supplied input files.
#
# Overall flow:
#   1. Read parameters / prompt for the license and data path.
#   2. Download data file(s), the shared object(s), and the wrapper via the Melissa Updater.
#   3. Confirm the shared object(s) are present.
#   4. Build the project, then run it (supplied input files or interactive).
#
# Options:
#   --global <value>    Path to the Global input file to deduplicate.
#   --us <value>        Path to the U.S. input file to deduplicate.
#   --dataPath <value>  Path to an existing data files directory. If omitted, the script
#                       prompts for a path; pressing Enter at that prompt skips it and
#                       downloads the data files into the project's Data folder via the
#                       Melissa Updater. A path that does not exist aborts the script.
#   --license <value>   License string. Resolved in this order:
#                         1. This option.
#                         2. An interactive prompt, if the option was not supplied.
#                         3. The MD_LICENSE environment variable, if the prompt was left blank.
#                       Note that the environment variable is the last resort, not the first:
#                       running without --license always prompts, even when MD_LICENSE is set.
#   --quiet             Suppresses the Melissa Updater console output during downloads.
#
# Examples:
#   ./MelissaMatchupObjectGlobalLinuxDotnet.sh --license "your-license"
#   ./MelissaMatchupObjectGlobalLinuxDotnet.sh --global "MelissaMatchupGlobalSampleInput.txt" --us "MelissaMatchupUSSampleInput.txt" --license "your-license"

######################### Constants ##########################

RED='\033[0;31m' #RED
NC='\033[0m' # No Color

######################### Parameters ##########################

globalFile=""
usFile=""
dataPath=""
license=""
quiet="false"

while [ $# -gt 0 ] ; do
  case $1 in
    --global) 
        globalFile="$2"

        if [ "$globalFile" == "--us" ] || [ "$globalFile" == "--dataPath" ] || [ "$globalFile" == "--license" ] || [ "$globalFile" == "--quiet" ] || [ -z "$globalFile" ];
        then
            printf "${RED}Error: Missing an argument for parameter \'globalFile\'.${NC}\n"  
            exit 1
        fi  
        ;;
    --us) 
        usFile="$2"

        if [ "$usFile" == "--global" ] || [ "$usFile" == "--dataPath" ] || [ "$usFile" == "--license" ] || [ "$usFile" == "--quiet" ] || [ -z "$usFile" ];
        then
            printf "${RED}Error: Missing an argument for parameter \'global\'.${NC}\n"  
            exit 1
        fi  
        ;;	
    --dataPath) 
        dataPath="$2"
        
        if [ "$dataPath" == "--license" ] || [ "$dataPath" == "--quiet" ] || [ "$dataPath" == "--global" ] || [ "$dataPath" == "--us" ] || [ -z "$dataPath" ];
        then
            printf "${RED}Error: Missing an argument for parameter \'dataPath\'.${NC}\n"  
            exit 1
        fi  
        ;;
    --license) 
        license="$2"

        if [ "$license" == "--global" ] || [ "$license" == "--us" ] || [ "$license" == "--dataPath" ] || [ "$license" == "--quiet" ] || [ -z "$license" ];
        then
            printf "${RED}Error: Missing an argument for parameter \'license\'.${NC}\n"  
            exit 1
        fi    
        ;;
    --quiet) 
        quiet="true" 
        ;;
  esac
  shift
done

######################### Config ###########################

# Product release the updater pulls files for
RELEASE_VERSION='2026.Q3'
ProductName="GLOBAL_MU_DATA"

# Uses the location of the .sh file 
CurrentPath=$(pwd)
ProjectPath="$CurrentPath/MelissaMatchupObjectGlobalLinuxDotnet"

BuildPath="$ProjectPath/Build"
if [ ! -d "$BuildPath" ];
then
    mkdir "$BuildPath"
fi

if [ -z "$dataPath" ];
then
    DataPath="$ProjectPath/Data"
else
    DataPath=$dataPath
fi

if [ ! -d "$DataPath" ] && [ "$DataPath" == "$ProjectPath/Data" ];
then
    mkdir "$DataPath"
elif [ ! -d "$DataPath" ] && [ "$DataPath" != "$ProjectPath/Data" ];
then
    printf "\nData file path does not exist. Please check that your file path is correct.\n"
    printf "\nAborting program, see above.\n"
    exit 1
fi

# Binary/shared object(s) needed to run the example
Config1_FileName="libmdMatchup.so"
Config1_ReleaseVersion=$RELEASE_VERSION
Config1_OS="LINUX"
Config1_Compiler="GCC48"
Config1_Architecture="64BIT"
Config1_Type="BINARY"

Config2_FileName="libmdGlobalParse.so"
Config2_ReleaseVersion=$RELEASE_VERSION
Config2_OS="LINUX"
Config2_Compiler="GCC48"
Config2_Architecture="64BIT"
Config2_Type="BINARY"

# C# wrapper source that exposes the shared object(s) to the .NET project
Wrapper_FileName="mdMatchup_cSharpCode.cs"
Wrapper_ReleaseVersion=$RELEASE_VERSION
Wrapper_OS="ANY"
Wrapper_Compiler="NET"
Wrapper_Architecture="ANY"
Wrapper_Type="INTERFACE"

######################## Functions #########################

# Download the product data file(s) into $DataPath via the Melissa Updater.
DownloadDataFiles()
{
    printf "\n============================== MELISSA UPDATER ============================\n"
    printf "MELISSA UPDATER IS DOWNLOADING DATA FILE(S)...\n"

    ./MelissaUpdater/MelissaUpdater manifest -p $ProductName -r $RELEASE_VERSION -l $1 -t $DataPath 

    if [ $? -ne 0 ];
    then
        printf "\nCannot run Melissa Updater. Please check your license string!\n"
        exit 1
    fi     
    
    printf "Melissa Updater finished downloading data file(s)!\n"
}

# Download the shared object(s) into the Build folder.
DownloadSO() 
{
    printf "\nMELISSA UPDATER IS DOWNLOADING SO(S)...\n"
    
    # Check for quiet mode
    if [ $quiet == "true" ];
    then
        ./MelissaUpdater/MelissaUpdater file --filename $Config1_FileName --release_version $Config1_ReleaseVersion --license $1 --os $Config1_OS --compiler $Config1_Compiler --architecture $Config1_Architecture --type $Config1_Type --target_directory $BuildPath &> /dev/null
        if [ $? -ne 0 ];
        then
            printf "\nCannot run Melissa Updater. Please check your license string!\n"
            exit 1
        fi

        printf "Melissa Updater finished downloading $Config1_FileName!\n"

        ./MelissaUpdater/MelissaUpdater file --filename $Config2_FileName --release_version $Config2_ReleaseVersion --license $1 --os $Config2_OS --compiler $Config2_Compiler --architecture $Config2_Architecture --type $Config2_Type --target_directory $BuildPath &> /dev/null
        if [ $? -ne 0 ];
        then
            printf "\nCannot run Melissa Updater. Please check your license string!\n"
            exit 1
        fi

        printf "Melissa Updater finished downloading $Config2_FileName!\n"
    else
        ./MelissaUpdater/MelissaUpdater file --filename $Config1_FileName --release_version $Config1_ReleaseVersion --license $1 --os $Config1_OS --compiler $Config1_Compiler --architecture $Config1_Architecture --type $Config1_Type --target_directory $BuildPath
        if [ $? -ne 0 ];
        then
            printf "\nCannot run Melissa Updater. Please check your license string!\n"
            exit 1
        fi

        printf "Melissa Updater finished downloading $Config1_FileName!\n"

        ./MelissaUpdater/MelissaUpdater file --filename $Config2_FileName --release_version $Config2_ReleaseVersion --license $1 --os $Config2_OS --compiler $Config2_Compiler --architecture $Config2_Architecture --type $Config2_Type --target_directory $BuildPath &> /dev/null
        if [ $? -ne 0 ];
        then
            printf "\nCannot run Melissa Updater. Please check your license string!\n"
            exit 1
        fi

        printf "Melissa Updater finished downloading $Config2_FileName!\n"
    fi
}

# Download the C# wrapper source into the project folder.
DownloadWrapper() 
{
    printf "\nMELISSA UPDATER IS DOWNLOADING WRAPPER(S)...\n"
    
    # Check for quiet mode
    if [ $quiet == "true" ];
    then
        ./MelissaUpdater/MelissaUpdater file --filename $Wrapper_FileName --release_version $Wrapper_ReleaseVersion --license $1 --os $Wrapper_OS --compiler $Wrapper_Compiler --architecture $Wrapper_Architecture --type $Wrapper_Type --target_directory $ProjectPath &> /dev/null     
        if [ $? -ne 0 ];
        then
            printf "\nCannot run Melissa Updater. Please check your license string!\n"
            exit 1
        fi
    else
        ./MelissaUpdater/MelissaUpdater file --filename $Wrapper_FileName --release_version $Wrapper_ReleaseVersion --license $1 --os $Wrapper_OS --compiler $Wrapper_Compiler --architecture $Wrapper_Architecture --type $Wrapper_Type --target_directory $ProjectPath 
        if [ $? -ne 0 ];
        then
            printf "\nCannot run Melissa Updater. Please check your license string!\n"
            exit 1
        fi
    fi
    
    printf "Melissa Updater finished downloading $Wrapper_FileName!\n"
}

# Verify the expected shared object(s) landed in the Build folder
CheckSOs() 
{
    if [ ! -f $BuildPath/$Config1_FileName ];
    then
        echo "false"
    elif [ ! -f $BuildPath/$Config2_FileName ];
    then    
        echo "false"
    else
        echo "true"
    fi
}

########################## Main ############################
printf "\n====================== Melissa MatchUp Object Global ======================\n                         [ .NET | Linux | 64BIT ]\n"

# Get license (either from parameters or user input)
if [ -z "$license" ];
then
    printf "Please enter your license string: "
    read license
fi

# Check for License from Environment Variables 
if [ -z "$license" ];
then
    license=`echo $MD_LICENSE` 
fi

if [ -z "$license" ];
then
    printf "\nLicense String is invalid!\n"
    exit 1
fi

# Get data file path (either from parameters or user input)
if [ "$DataPath" = "$ProjectPath/Data" ]; then
    printf "Please enter your data files path directory if you have already downloaded the release zip.\nOtherwise, the data files will be downloaded using the Melissa Updater (Enter to skip): "
    read dataPathInput

    if [ ! -z "$dataPathInput" ]; then  
        if [ ! -d "$dataPathInput" ]; then  
            printf "\nData file path does not exist. Please check that your file path is correct.\n"
            printf "\nAborting program, see above.\n"
            exit 1
        else
            DataPath=$dataPathInput
        fi
    fi
fi

# Use Melissa Updater to download data file(s) 
# Download data file(s) 
DownloadDataFiles $license # Comment out this line if using own release

# Download SO(s)
DownloadSO $license 

# Download wrapper(s)
DownloadWrapper $license

# Check if all SO(s) have been downloaded. Exit script if missing
printf "\nDouble checking SO file(s) were downloaded...\n"

SOsAreDownloaded=$(CheckSOs)

if [ "$SOsAreDownloaded" == "false" ];
then
    printf "\nMissing data file(s).  Please check that your license string and directory are correct.\n"

    printf "\nAborting program, see above.\n"
    exit 1
fi

printf "\nAll file(s) have been downloaded/updated!\n"

# Start program
# Build project
printf "\n=============================== BUILD PROJECT =============================\n"

dotnet publish -f="net10.0" -c Release -o $BuildPath MelissaMatchupObjectGlobalLinuxDotnet/MelissaMatchupObjectGlobalLinuxDotnet.csproj

# Run Project
# No input file supplied -> run interactively; otherwise pass the input files in.
if [ -z "$globalFile" ] && [ -z "$usFile" ];
then
    export LD_LIBRARY_PATH=$LD_LIBRARY_PATH:./Build

    cd MelissaMatchupObjectGlobalLinuxDotnet
    dotnet $BuildPath/MelissaMatchupObjectGlobalLinuxDotnet.dll --license $license --dataPath $DataPath
    cd ..
else
    export LD_LIBRARY_PATH=$LD_LIBRARY_PATH:./Build

    cd MelissaMatchupObjectGlobalLinuxDotnet
    dotnet $BuildPath/MelissaMatchupObjectGlobalLinuxDotnet.dll --license $license --dataPath $DataPath --global "$globalFile" --us "$usFile"
    cd ..
fi
