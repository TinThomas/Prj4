<template>
    <div class="Printhere">
        <span v-html="HTMLcontent">

        </span>
        <button v-on:click="getItems()">Click me</button>
    </div>
</template>

<script>
    import axios from 'axios';
    export default {
        data() {
            return {
                HTMLcontent : null
            }
        },
        methods: {
            getItems() {
                var ref = this;
                axios.get('http://localhost:5000/Home/item')
                    .then(function (response) {
                        if (response.status != 200) {
                            window.console.log("Something went wrong " + response.status)
                        }
                        window.console.log(response.data);
                         var object = response.data;
                        for (var i = 0; i < response.data.length; i++) {
                             ref.HTMLcontent += '<p>' + object[i].Title + '</p>';
                        }
                    }).catch(function (err) {
                        window.console.log(err);
                    });
            }
        }
    }
</script>