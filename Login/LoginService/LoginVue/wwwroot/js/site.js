let obj = document.getElementsByClassName("menu");
for (i = 0; i < obj.length; i++) {
    obj[i].addEventListener("mouseover", function (event) {
        let target = event.target || event.srcElement;
        target.className = "menuEnhanced";
    });
    obj[i].addEventListener("mouseout", function (event) {
        let target = event.target || event.srcElement;
        target.className = "menu";
    });
}

// Turn menu navigation into AJAX calls 
let ancors = document.getElementsByClassName("doAjax");
for (let i = 0; i < ancors.length; i++)
    ancors[i].addEventListener('click', doFetchCall);

function doFetchCall(eventArg) {
    eventArg.preventDefault();

    var url = eventArg.target.href;
    fetch(url, {
            headers: { 'X-Requested-With': 'Fetch' },
            method: 'GET'
        })
        .then(function (response) {
            if (response.status !== 200) {
                console.log('Looks like there was a problem. Status Code: ' + response.status);
                return;
            }
            // Change innerHtml of main
            response.text()
                .then(function (data) {
                    let mainEl = document.getElementById('content');
                    mainEl.innerHTML = data;
                    if (url.toLowerCase().includes('/home/music'))
                        LoadJavaScript('/js/music-vue.js');
                    else if (url.toLowerCase().includes('/jobs/create'))
                        LoadJavaScript('/js/job-vue.js');
                });
        })
        .catch(function (err) {
            console.log('Fetch Error: -S', err);
        });
}

function doAjaxCall(eventArg) {
    eventArg.preventDefault();
    let url = eventArg.target.href;

    let req = new XMLHttpRequest();
    req.addEventListener("load", transferComplete);
    req.addEventListener("error", transferFailed);
    req.open("GET", url, true);
    req.setRequestHeader('X-Requested-With', 'XMLHttpRequest');
    req.send(null);

    function transferFailed(evt) {
        alert("An error occurred while transferring the file.");
    }

    function transferComplete(evt) {
        let mainEl = document.getElementById('content');
        mainEl.innerHTML = req.responseText;
    }
}

vm = new Vue({
    el: '#Nav',
    data: function() {
        return {
            isUser: false
        }
    },
    computed: {
        isActive: function() {
            return {
                active: this.isUser
            }
        }
    },
    method: {
        checkUser: function() {
            return {
                isUser
        }
        }
    }
});
