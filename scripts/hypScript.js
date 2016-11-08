
var myWidth = 0;
var myHeight = 0;

var active_menu = "";




$(document).ready(function () {

    Calculate_Screen_Dimensions();

    Adjust_Hide_Back_Div();



    if (active_menu != ".mHypster") {
        $(".mHypster").bind('mouseenter', function () {
            $(".mHypster").css("background-color", "#191919");
        });
        $(".mHypster").bind('mouseout', function () {
            $(".mHypster").css("background", "");
        });
    }

    if (active_menu != ".mListen") {
        $(".mListen").bind('mouseenter', function () {
            $(".mListen").css("background-color", "#191919");
        });
        $(".mListen").bind('mouseout', function () {
            $(".mListen").css("background", "");
        });
    }

    if (active_menu != ".mCreate") {
        $(".mCreate").bind('mouseenter', function () {
            $(".mCreate").css("background-color", "#191919");
        });
        $(".mCreate").bind('mouseout', function () {
            $(".mCreate").css("background", "");
        });
    }

    if (active_menu != ".mConnect") {
        $(".mConnect").bind('mouseenter', function () {
            $(".mConnect").css("background-color", "#191919");
        });
        $(".mConnect").bind('mouseout', function () {
            $(".mConnect").css("background", "");
        });
    }







    //------------------------------------------------------------------------------------------------------------------
    $(".txtLogin").focus(function () {
        $(this).css("background-color", "#fdf1b0");
        $(this).css("font-weight", "bold");
    });

    $(".txtLogin").blur(function () {
        $(this).css("background-color", "#EEEEEE");
        $(this).css("font-weight", "normal");
    });
    //------------------------------------------------------------------------------------------------------------------


});



$(window).resize(function () {
    Adjust_Hide_Back_Div();
});
//------------------------------------------------------------------------------------------------------------------








//------------------------------------------------------------------------------------------------------------------
var player_ins = null;
function OpenExsPlayer() {
    if (player_ins != null) {
        player_ins.focus();
    }
    else {
        OpenPlayer("media_type=DEFPL");
    }
}
//------------------------------------------------------------------------------------------------------------------









function subMouseOver(sender) {
    $(sender).css('background-color', '#173860');
}

function subMouseOut(sender) {
    $(sender).css('background-color', '');
}













var isMobile = {
    Android: function () {
        return navigator.userAgent.match(/Android/i);
    },
    BlackBerry: function () {
        return navigator.userAgent.match(/BlackBerry/i);
    },
    iOS: function () {
        return navigator.userAgent.match(/iPhone|iPad|iPod/i);
    },
    Opera: function () {
        return navigator.userAgent.match(/Opera Mini/i);
    },
    Windows: function () {
        return navigator.userAgent.match(/IEMobile/i);
    },
    any: function () {
        return (isMobile.Android() || isMobile.BlackBerry() || isMobile.iOS() || isMobile.Opera() || isMobile.Windows());
    }
};

















function OpenPlayer(params) {
    var player_W = 1050;
    var player_H = 700;

    if (myWidth < 1050)
        player_W = myWidth;

    if (myHeight < 700) {
        player_H = myHeight;
        player_W = 1065;
    }


    var myWindow;
    myWindow = window.open('/hypsterPlayer/MPL?' + params, 'HypsterPlayer', 'location=0, status=0, scrollbars=0, width=' + player_W + ', height=' + player_H);
    myWindow.focus();
}





function Calculate_Screen_Dimensions() {
    myWidth = $(window).width();
    myHeight = $(window).height();
}



function Adjust_Hide_Back_Div() {
    var hdiv = document.getElementById("HideBackDiv");
    hdiv.style.width = $(document).width() + 'px';
    hdiv.style.height = $(document).height() + 'px';
}






//****************************************************************************************

function Show_Content() {
    document.getElementById('HideBackDiv').style.display = 'none';
    document.getElementById('popupContainer').style.display = 'none';
    $('#popupContainer').html("");
}

function Hide_Content() {
    document.getElementById('HideBackDiv').style.display = 'block';
    document.getElementById('popupContainer').style.display = 'block';
}




function Show_Content_PopupPlayer() {
    document.getElementById('HideBackDiv_PopupPlayer').style.display = 'none';
    document.getElementById('popupContainer_PopupPlayer').style.display = 'none';
    $('#popupContainer').html("");

    $("#MainContHolder").css("visibility", "visible");
}

function Hide_Content_PopupPlayer() {

    var hdiv = document.getElementById("HideBackDiv_PopupPlayer");
    hdiv.style.width = $(document).width() + 'px';
    hdiv.style.height = $(document).height() + 'px';

    
    document.getElementById('HideBackDiv_PopupPlayer').style.display = 'block';
    document.getElementById('popupContainer_PopupPlayer').style.display = 'block';

    $("#MainContHolder").css("visibility", "hidden");
}




function getQuerystring(key, default_) {
    if (default_ == null) default_ = "";
    key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
    var qs = regex.exec(window.location.href);
    if (qs == null)
        return default_;
    else
        return qs[1];
}


function getQuerystring_str(key, default_) {
    
    key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
    var qs = regex.exec(default_);
    if (qs == null)
        return "";
    else
        return qs[1];
}
//****************************************************************************************






// -----SLIDESHOW-----
//****************************************************************************************
function StartSlideshow() {
    $('.mainSlideshow').cycle({
        fx: 'scrollRight',
        speed: 500,
        timeout: 9000
    });
}

function opnSlide(id) {
    $('.mainSlideshow').cycle('pause');

    $('#HomeSlide1').css('display', 'none');
    $('#HomeSlide2').css('display', 'none');
    $('#HomeSlide3').css('display', 'none');

    $('#HomeSlide' + id).css('visibility', 'visible');
    $('#HomeSlide' + id).css('display', 'block');
    $('#HomeSlide' + id).css('left', '0px');
}
//****************************************************************************************







//****************************************************************************************
function ShowImage(image_src) {
    Hide_Content();


    $(document).scrollTop(0);

    var close_btn = "";
    close_btn += "<img alt=\"\" src=\"/imgs/exit_button.png\" style=\" float:right; margin:10px 20px 0 0; cursor:pointer;\" onclick=\"Show_Content();\" />";


    $('#popupContainer').html("<div style=' width:" + $(window).width() + "px; '><center> <div>" + close_btn + " <img alt='' src='" + image_src + "' style=' margin-top:60px; border:3px solid #ABABAB; border-radius:3px;' /></div>    </center> </div>");
    
}


function ShowImageViewer(username, active_image) {
    Hide_Content();

    
    $(document).scrollTop(0);

    $.ajax({
        type: "POST",
        url: "/account/pr_PicturesViewer?username=" + username + "&imgid=" + active_image,
        async: true,
        success: function (data) {
            $('#popupContainer').html(data);
        }
    });
}







function ShowAddToMyPlaylist(song_id_url) {
    Hide_Content();

    $(document).scrollTop(0);


    try {
        if ($("#txtSearchString") != null) {
            var ss = $("#txtSearchString").val();

            if (ss != " Enter Artist, Song or Genre") {
                ss = ss.replace(" ", "+");
                ss = ss.replace("/", "+");
                ss = ss.replace("\\", "+");
                ss = ss.replace("&", "+");
                ss = ss.replace("$", "+");
                ss = ss.replace("?", "+");
                ss = ss.replace("!", "+");
                song_id_url += "&ss=" + ss;
            }
        }
    } catch (ex) { }


    $.ajax({
        type: "POST",
        url: "/playlist/AddToPlayList" + song_id_url,
        async: true,
        success: function (data) {
            $('#popupContainer').html(data);
        }
    });
}


function ShowAddToMyPlaylistPopup(song_id_url) {
    Hide_Content_PopupPlayer();

    $(document).scrollTop(0);

    $.ajax({
        type: "POST",
        url: "/playlist/AddToPlayListPopup" + song_id_url,
        async: true,
        success: function (data) {
            $('#popupContainer_PopupPlayer').html(data);
        }
    });
}




function ShowLoginRegister(song_id_url) {
    Hide_Content();

    $(document).scrollTop(0);
    
    $.ajax({
        type: "POST",
        url: "/account/Register",
        async: true,
        success: function (data) {
            $('#popupContainer').html(data);
        }
    });
}












/* HOME */
/* ------------------------------------------------------------------ */


function StartMusicSearch() {
    var ss_str = $("#txtSearchString").val();

    if (ss_str != " Enter Artist, Song or Genre") {
        ss_str = ss_str.replace(" ", "+");
        ss_str = ss_str.replace("/", "+");
        ss_str = ss_str.replace("\\", "+");
        ss_str = ss_str.replace("&", "+");
        ss_str = ss_str.replace("$", "+");
        ss_str = ss_str.replace("?", "+");
        ss_str = ss_str.replace("!", "+");
        window.location = "/listen?ss=" + ss_str;
    }
}

function SearchString_KeyUp(e) {

    if (window.event) {

        if (window.event.keyCode == 13) {
            StartMusicSearch();
        }
    }
    else {

        if (e.which == 13) {
            StartMusicSearch();
        }
    }

}

/* ------------------------------------------------------------------ */













/* LISTEN */
/* ------------------------------------------------------------------ */

function Open_Menu1() {

    $('#listenTab1').css('display', 'block');
    $('#listenTab2').css('display', 'none');
    $('#listenTab3').css('display', 'none');
    $('#Menu1').css('background-color', '#1d1d1c');
    $('#Menu2').css('background-color', '#2b3032');
    $('#Menu3').css('background-color', '#2b3032');
}


function Open_Menu2() {

    $('#listenTab1').css('display', 'none');
    $('#listenTab2').css('display', 'block');
    $('#listenTab3').css('display', 'none');
    $('#Menu1').css('background-color', '#2b3032');
    $('#Menu2').css('background-color', '#1d1d1c');
    $('#Menu3').css('background-color', '#2b3032');
}

function Open_Menu3() {

    $('#listenTab1').css('display', 'none');
    $('#listenTab2').css('display', 'none');
    $('#listenTab3').css('display', 'block');
    $('#Menu1').css('background-color', '#2b3032');
    $('#Menu2').css('background-color', '#2b3032');
    $('#Menu3').css('background-color', '#1d1d1c');
}



function CustomRadioStation() {
    var station_name = "";
    station_name = $("#stationName").val();
    station_name = station_name.replace(" ", "+");
    OpenPlayer('media_type=Radio&Genre=' + station_name + "&search=1");
}

/* ------------------------------------------------------------------ */













/* ------------------------------------------------------------------ */


var isOrderOn = "";

function SearchMusic() {

    var ss = $("#txtSearchString").val();

    
    if ($("input[name=group_orderBy]:radio").val() != undefined) {
        isOrderOn = $("input[name=group_orderBy]:radio").val();
    }

    SearchMusicStr(ss);
}



function SearchMusicStr(search_string) {

    $('#listenSlideContHolder').html("<div style='float:right; width:1024px;'><span style='font-size:24px; color:#454545;'>Searching...</span></div>");

    var ss = search_string;
    ss = ss.replace(" ", "+");
    ss = ss.replace("/", "+");
    ss = ss.replace("\\", "+");
    ss = ss.replace("&", "+");
    ss = ss.replace("$", "+");
    ss = ss.replace("?", "+");
    ss = ss.replace("!", "+");


    var search_url = "/search/Music?ss=" + ss;
    if (isOrderOn != "") {
        search_url += "&orderBy=" + isOrderOn;
    }


    $.ajax({
        type: "POST",
        url: search_url,
        async: true,
        success: function (data) {
            $('#listenSlideContHolder').html(data);
        }
    });

    $(document).scrollTop(640);
}



function SearchMusicStrPage(search_string, page) {

    $('#listenSlideContHolder').html("<div style='float:right; width:1024px;'><span style='font-size:24px; color:#454545;'>Searching...</span></div>");

    var ss = search_string;
    ss = ss.replace(" ", "+");
    ss = ss.replace("/", "+");
    ss = ss.replace("\\", "+");
    ss = ss.replace("&", "+");
    ss = ss.replace("$", "+");
    ss = ss.replace("?", "+");
    ss = ss.replace("!", "+");

    var search_url = "/search/Music?ss=" + ss + "&page=" + page;
    if (isOrderOn != "") {
        search_url += "&orderBy=" + isOrderOn;
    }

    $.ajax({
        type: "POST",
        url: search_url,
        async: true,
        success: function (data) {
            $('#listenSlideContHolder').html(data);
        }
    });

    $(document).scrollTop(640);
}






function SearchMusicYTID() {

    var ss = $("#txtSearchString_youtube").val();
    ss = ss.replace(" ", "+");


    $('#listenSlideContHolder').html("<div style='float:right; width:1024px;'><span style='font-size:24px; color:#454545;'>Searching...</span></div>");


    $.ajax({
        type: "POST",
        url: "/search/MusicYTID?ss=" + ss,
        async: true,
        success: function (data) {
            $('#listenSlideContHolder').html(data);
        }
    });

    $(document).scrollTop(640);
}







function ListenSearchString_KeyUp(e) {
    
    if (window.event) {

        if (window.event.keyCode == 13) {
            SearchMusic();
        }
    }
    else {

        if (e.which == 13) {
            SearchMusic();
        }
    }
}

/* ------------------------------------------------------------------ */







/* CREATE */
/* ------------------------------------------------------------------ */



function shrinkAllSLides() {
    $("#CreateSection1").css("height", "140px");
    $("#CreateSection2").css("height", "140px");
    $("#CreateSection3").css("height", "140px");

    $("#CreateSection1").css("background-color", "#2b3032");
    $("#CreateSection2").css("background-color", "#2b3032");
    $("#CreateSection3").css("background-color", "#2b3032");


    $("#CreateSection1_img").css("display", "none");
    $("#CreateSection2_img").css("display", "none");
    $("#CreateSection3_img").css("display", "none");


    $("#CreateContHolder").css("display", "block");
    $("#CreateContHolder").css("min-height", "450px");

    $("#CreateContHolder").html("");
}



/* ------------------------------------------------------------------ */








/* CREATE PLAYER */
/* ------------------------------------------------------------------ */

function LoadPlayerCont(cont_url) {
    //--------------------------------------------------------
    //load playlists
    $.ajax({
        type: "POST",
        url: cont_url,
        async: true,
        success: function (data) {
            $('#CreateContHolder').html(data);
        }
    });
    //-------------------------------------------------------
}


function playlist_changed(object) {

    $.ajax({
        type: "POST",
        url: "/playlist/getPlaylistDetailsEdt?PL_ID=" + object[object.selectedIndex].value + "&PL_TYPE=BarPlayer",
        async: true,
        success: function (data) {
            $('#PlaylistSongsContainer').html(data);
        }
    });

}


function preview_skin_bar(skin_id) {
    $('#skinImgHolder1').attr('src', '/imgs/get_player/bar_skins/' + skin_id + ".png");
}

function preview_skin_classic(skin_id) {

    if (skin_id == "6") {
        $("#CustomizePlayerContH").css("display", "block");
    }
    else {
        $('#skinImgHolder1').attr('src', '/imgs/get_player/classic_skins/' + skin_id + ".png");
        $("#CustomizePlayerContH").css("display", "none");
    }

}

/* ------------------------------------------------------------------ */









/* CREATE RADIO */
/* ------------------------------------------------------------------ */
function PopulateRadioStationF(val_str) {
    $('#RadioStationSearchTerm').val(val_str);
}
/* ------------------------------------------------------------------ */









/* ------------------------------------------------------------------ */
function showHide_AddSearch(caller) {

    if ($("#AddSearch").css("display") == "none") {
        $("#AddSearch").css("display", "block");
        $("#MainSearchLn").css("height", "130px;");

        $(".contMiddle").css("display", "none");

        caller.innerHTML = "Hide";

        $("#txtSearchString").css("background-color", "#DDDDDD");
        $("#txtSearchString").css("border", "3px solid #DDDDDD");
    }
    else {
        $("#AddSearch").css("display", "none");
        $("#MainSearchLn").css("height", "70px;");

        $(".contMiddle").css("display", "block");

        caller.innerHTML = "Advanced Search";

        $("#txtSearchString").css("background-color", "#FFFFFF");
        $("#txtSearchString").css("border", "3px solid #FFFFFF");
    }
}
/* ------------------------------------------------------------------ */





