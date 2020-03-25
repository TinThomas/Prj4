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