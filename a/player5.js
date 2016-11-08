
//**************************************************************************************************************************
var imgs_f = "imgs5";
var PL_CREAETED = false;
var hypHostName = "hypster.com";
var hypMainBackColor = "#171717";
var hypUserID = 0;
var hypPlaylistID = 0;
var hypPlayerControls = 0;
var hypPlayerSort = "";
var hypButtonsBack = "#353535";
var hypAutostart = 0;
var hypVisibility = "visible";
var hypSaveState = 1;


if (typeof jQuery == 'undefined') {
    var tag_jquery = document.createElement('script');
    tag_jquery.src = "//code.jquery.com/jquery-1.8.3.min.js";
    var firstScriptTag_jquery = document.getElementsByTagName('script')[0];
    firstScriptTag_jquery.parentNode.insertBefore(tag_jquery, firstScriptTag_jquery);
}


var tag = document.createElement('script');
tag.src = "//www.youtube.com/iframe_api";
var firstScriptTag = document.getElementsByTagName('script')[0];
firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);
//**************************************************************************************************************************










//**************************************************************************************************************************

var player;
function onYouTubeIframeAPIReady() {
    InitPlayer();        
}



function InitPlayer() {
    GeneratePlayerControls();

        
    player = new YT.Player('hypPlayer', {
        width: '200',
        height: '200',
        videoId: '',
        playerVars: { 'autoplay': 0, 'controls': 0 },
        events: {
            'onReady': onPlayerReady,
            'onStateChange': onPlayerStateChange,
            'onError': onError
        }
    });

}



var player_height = 227;
function GeneratePlayerControls() {

    //-------------------------------------------------------------------------------
    $('#hypPlayerCont').remove();
    $('.hypPlayerAddCons').html("");
    //-------------------------------------------------------------------------------

    

    //get varibles
    //-------------------------------------------------------------------------------
    hypHostName = $("#HypPlayerScript").attr("hypHostName");
    hypUserID = $("#HypPlayerScript").attr("hypUserID");
    hypPlaylistID = $("#HypPlayerScript").attr("hypPlaylistID");
    hypMainBackColor = $("#HypPlayerScript").attr("hypMainBackColor");
    hypPlayerControls = $("#HypPlayerScript").attr("hypPlayerControls");
    hypPlayerSort = $("#HypPlayerScript").attr("hypPlayerSort");
    hypButtonsBack = $("#HypPlayerScript").attr("hypButtonsBack");
    hypAutostart = $("#HypPlayerScript").attr("hypAutostart");
    hypVisibility = $("#HypPlayerScript").attr("hypVisibility");
    hypSaveState = $("#HypPlayerScript").attr("hypSaveState");
    //-------------------------------------------------------------------------------




    //check settings
    //-------------------------------------------------------------------------------
    if (hypPlayerControls == 0) {
        player_height = 200;
    }
    else {
        player_height = 227;
    }
    //-------------------------------------------------------------------------------




    //create base elements
    //-------------------------------------------------------------------------------
    var tag_player_cont = document.createElement('div');
    $(tag_player_cont).attr("id", "hypPlayerCont");
    $(tag_player_cont).css("clear", "both");
    $(tag_player_cont).css("width", "200px");
    $(tag_player_cont).css("height", player_height + "px");
    $(tag_player_cont).css("background-color", hypMainBackColor); //SET COLOR HERE
    $(tag_player_cont).css("visibility", "visible"); //hypVisibility
    $(tag_player_cont).css("overflow", "hidden");

    if (hypVisibility == "hidden") {
        $(tag_player_cont).css("width", "2px");
        $(tag_player_cont).css("height", "2px");
        $(tag_player_cont).css("filter", "progid:DXImageTransform.Microsoft.Alpha(opacity=10);");
        $(tag_player_cont).css("opacity", "0.10;");
        $(tag_player_cont).css("-moz-opacity", "0.10;");
    }

    var insert_to = document.getElementById('HypPlayerScript');
    insert_to.parentNode.insertBefore(tag_player_cont, insert_to);


    $("#hypPlayerCont").append("<div id='hypPlayer' style='float:left; width:200px; height:200px;' />");
    //-------------------------------------------------------------------------------




    //create controls bar for player
    //-------------------------------------------------------------------------------
    if (hypPlayerControls == 1) {
        GenerateControlsBar();
    }
    //-------------------------------------------------------------------------------




    //check if aditional player controls are defined
    //-------------------------------------------------------------------------------
    if ($('.hypPlayerAddCons').length > 0) {
        GenerateControlsBar_Additional();
    };
    //-------------------------------------------------------------------------------

}


function GenerateControlsBar() {

    $("#hypPlayerCont").append("<div id='hypPlayerMenu' style='float:left; width:200px; height:27px;' />");


    var btn_style = "float:left; width:24px; height:23px; line-height:23px; margin:2px 0 0 2px; border-radius:3px; text-align:center; font-size:16px; color:#FFFFFF; cursor:pointer; font-weight:bold;";
    $("#hypPlayerMenu").append("<div class=\"hypPlayer_Prev_btn\" onclick=\"PlayPrevVideo();\" style=\" " + btn_style + " background:" + hypButtonsBack + " url('http://" + hypHostName + "/a/" + imgs_f + "/prev_icn.png') no-repeat center;\" />");
    $("#hypPlayerMenu").append("<div class=\"hypPlayer_PlPs_btn\" onclick=\"PlayPauseVideo();\" style=\" " + btn_style + " background:" + hypButtonsBack + " url('http://" + hypHostName + "/a/" + imgs_f + "/play_icn.png') no-repeat center;\" />");
    $("#hypPlayerMenu").append("<div class=\"hypPlayer_Next_btn\" onclick=\"PlayNextVideo();\" style=\" " + btn_style + " background:" + hypButtonsBack + " url('http://" + hypHostName + "/a/" + imgs_f + "/next_icn.png') no-repeat center;\" />");
    $("#hypPlayerMenu").append("<div class=\"hypPlayer_Shuffle_btn\" onclick=\"ShuffleSongs();\" style=\" " + btn_style + " background:" + hypButtonsBack + " url('http://" + hypHostName + "/a/" + imgs_f + "/shuffle_icn.png') no-repeat center;\" />");
    $("#hypPlayerMenu").append("<div class=\"hypPlayer_Mute_btn\" onclick=\"MuteSound();\" style=\" " + btn_style + " background:" + hypButtonsBack + " url('http://" + hypHostName + "/a/" + imgs_f + "/mute_icn.png') no-repeat center;\" />");
    $("#hypPlayerMenu").append("<a class=\"hypPlayer_Hypster_btn\" href=\"http://hypster.com\" target=\"_blank\" style=\" display:block; " + btn_style + " background:" + hypButtonsBack + " url('http://" + hypHostName + "/a/" + imgs_f + "/hypster_icn.png') no-repeat center;\" ></a>");

}


function GenerateControlsBar_Additional() {

    $(".hypPlayerAddCons").css("clear", "both"); 
    $(".hypPlayerAddCons").css("width", "158px");
    $(".hypPlayerAddCons").css("height", "27px");
    $(".hypPlayerAddCons").css("visibility", "visible");
    $(".hypPlayerAddCons").css("background-color", hypMainBackColor); //SET COLOR HERE
    $(".hypPlayerAddCons").css("border-radius", "2px"); 


    var btn_style = "float:left; width:24px; height:23px; line-height:23px; margin:2px 0 0 2px; border-radius:3px; text-align:center; font-size:16px; color:#FFFFFF; cursor:pointer; font-weight:bold;";
    $(".hypPlayerAddCons").append("<div class=\"hypPlayer_Prev_btn\" onclick=\"PlayPrevVideo();\" style=\" " + btn_style + " background:" + hypButtonsBack + " url('http://" + hypHostName + "/a/" + imgs_f + "/prev_icn.png') no-repeat center;\" />");
    $(".hypPlayerAddCons").append("<div class=\"hypPlayer_PlPs_btn\" onclick=\"PlayPauseVideo();\" style=\" " + btn_style + " background:" + hypButtonsBack + " url('http://" + hypHostName + "/a/" + imgs_f + "/play_icn.png') no-repeat center;\" />");
    $(".hypPlayerAddCons").append("<div class=\"hypPlayer_Next_btn\" onclick=\"PlayNextVideo();\" style=\" " + btn_style + " background:" + hypButtonsBack + " url('http://" + hypHostName + "/a/" + imgs_f + "/next_icn.png') no-repeat center;\" />");
    $(".hypPlayerAddCons").append("<div class=\"hypPlayer_Shuffle_btn\" onclick=\"ShuffleSongs();\" style=\" " + btn_style + " background:" + hypButtonsBack + " url('http://" + hypHostName + "/a/" + imgs_f + "/shuffle_icn.png') no-repeat center;\" />");
    $(".hypPlayerAddCons").append("<div class=\"hypPlayer_Mute_btn\" onclick=\"MuteSound();\" style=\" " + btn_style + " background:" + hypButtonsBack + " url('http://" + hypHostName + "/a/" + imgs_f + "/mute_icn.png') no-repeat center;\" />");
    $(".hypPlayerAddCons").append("<a class=\"hypPlayer_Hypster_btn\" href=\"http://hypster.com\" target=\"_blank\" style=\" display:block; " + btn_style + " background:" + hypButtonsBack + " url('http://" + hypHostName + "/a/" + imgs_f + "/hypster_icn.png') no-repeat center;\" ></a>");

}
//**************************************************************************************************************************


































/* PLAYER EVENTS */
//**************************************************************************************************************************
function onPlayerReady(event) {
    LoadPlaylist();
}

function onError(event) {
    PlayNextVideo();
}

var done = false;
function onPlayerStateChange(event) {
    if (event.data == YT.PlayerState.ENDED) {
        PlayNextVideo();
        return;
    }


    if (event.data == YT.PlayerState.PAUSED) {
        PLAY_STATE = "0";
        $(".hypPlayer_PlPs_btn").css("background-image", "url('http://" + hypHostName + "/a/" + imgs_f + "/play_icn.png')");
    }
    if (event.data == YT.PlayerState.PLAYING) {
        PLAY_STATE = "1";
        $(".hypPlayer_PlPs_btn").css("background-image", "url('http://" + hypHostName + "/a/" + imgs_f + "/pause_icn.png')");
    }
}
//**************************************************************************************************************************








//**************************************************************************************************************************
var items_arr = new Array();
var PLAY_STATE = "0";
var CURR_VIDEO = 0;
var SOUND_STATE = 1;



function LoadPlaylist() {

    $.ajax({
        type: "POST",
        url: "http://" + hypHostName + "/playlist/exPl_Playlist_Npl_1?PL_ID=" + hypPlaylistID + "&US_ID=" + hypUserID + "&Sort=" + hypPlayerSort,
        dataType: "jsonp",
        async: true,
        success: function (data) {


            var data_str = data;
            if (data_str[data_str.length - 1]) {
                data_str = data.substring(0, data_str.length - 1);
            }
            items_arr = data_str.split(',');


            CURR_VIDEO = 0;


            //overwrite default settings if user
            //--------------------------------------------------------------
            var cookie_playerState = getCookie("PlayPause");
            if (typeof cookie_playerState != "undefined" && hypSaveState == 1) {

                if (cookie_playerState == 1) {
                    StartPlayVideo();
                }
                return;
            }
            //--------------------------------------------------------------


            //--------------------------------------------------------------
            if (hypAutostart == 1) {
                StartPlayVideo();
            }
            //--------------------------------------------------------------

        }
    });
}







function PlayPrevVideo() {

    if (CURR_VIDEO > 0) {
        CURR_VIDEO = CURR_VIDEO - 1;
    }

    player.loadVideoById(items_arr[CURR_VIDEO], 0, 'default');

    PLAY_STATE = "1";
    $(".hypPlayer_PlPs_btn").css("background-image", "url('http://" + hypHostName + "/a/" + imgs_f + "/pause_icn.png')");

}



// initial start
function StartPlayVideo() {
    var start_time = 0;


    var saved_song = getCookie("CurrSong");
    var curr_time = getCookie("CurrTime");


    if (typeof curr_time != "undefined" && hypSaveState == 1) {
        start_time = curr_time;
    }


    if (typeof saved_song != "undefined" && hypSaveState == 1) {
        for (var i = 0; i < items_arr.length; i++) {
            if (items_arr[i] == saved_song) {
                CURR_VIDEO = i;
            }
        }
    }
    else {
        CURR_VIDEO = 0;
        start_time = 0;
    }


    player.loadVideoById(items_arr[CURR_VIDEO], start_time, 'default');


    PLAY_STATE = "1";
    $(".hypPlayer_PlPs_btn").css("background-image", "url('http://" + hypHostName + "/a/" + imgs_f + "/pause_icn.png')");


    if (CURR_VIDEO == items_arr.length - 1) {
        CURR_VIDEO = -1;
    }
}






function PlayNextVideo() {

    if (CURR_VIDEO < items_arr.length) {
        CURR_VIDEO = CURR_VIDEO + 1;
    }

    player.loadVideoById(items_arr[CURR_VIDEO], 0, 'default');

    PLAY_STATE = "1";
    $(".hypPlayer_PlPs_btn").css("background-image", "url('http://" + hypHostName + "/a/" + imgs_f + "/pause_icn.png')");


    if (CURR_VIDEO == items_arr.length - 1) {
        CURR_VIDEO = -1;
    }
}





function PlayPauseVideo() {

    if (PLAY_STATE == "0") { //IF NOT STARTED START
        player.playVideo();

        PLAY_STATE = "1";
        $(".hypPlayer_PlPs_btn").css("background-image", "url('http://" + hypHostName + "/a/" + imgs_f + "/pause_icn.png')");
    }
    else { //IF STARTED NEED TO STOP
        player.pauseVideo();

        PLAY_STATE = "0";
        $(".hypPlayer_PlPs_btn").css("background-image", "url('http://" + hypHostName + "/a/" + imgs_f + "/play_icn.png')");
    }
}




function ShuffleSongs() {
    LoadPlaylist();
}


function MuteSound() {
    if (SOUND_STATE == 1) {
        player.mute();
        SOUND_STATE = 0;

        $(".hypPlayer_Mute_btn").css("background-image", "url('http://" + hypHostName + "/a/" + imgs_f + "/unmute_icn.png')");
    }
    else {
        player.unMute();
        SOUND_STATE = 1;

        $(".hypPlayer_Mute_btn").css("background-image", "url('http://" + hypHostName + "/a/" + imgs_f + "/mute_icn.png')");
    }

}
//**************************************************************************************************************************









//**************************************************************************************************************************
window.onbeforeunload = function () {

    setCookie("PlayPause", PLAY_STATE, 7);
    if (typeof items_arr[CURR_VIDEO] != "undefined") {
        setCookie("CurrSong", items_arr[CURR_VIDEO], 7);
    }
    setCookie("CurrTime", player.getCurrentTime(), 7);
}
//**************************************************************************************************************************














/* SYS N/M */
//**************************************************************************************************************************
function setCookie(c_name, value, exdays) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + exdays);
    var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
    document.cookie = c_name + "=" + c_value;
}

function getCookie(c_name) {
    var i, x, y, ARRcookies = document.cookie.split(";");
    for (i = 0; i < ARRcookies.length; i++) {
        x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
        y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
        x = x.replace(/^\s+|\s+$/g, "");
        if (x == c_name) {
            return unescape(y);
        }
    }
}
//**************************************************************************************************************************





/* SYS N/M */
//**************************************************************************************************************************
var _gaq = _gaq || [];
_gaq.push(['_setAccount', 'UA-28695705-6']);
_gaq.push(['_setDomainName', 'hypster.com']);
_gaq.push(['_trackPageview']);

(function () {
    var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
    ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
})();
//**************************************************************************************************************************