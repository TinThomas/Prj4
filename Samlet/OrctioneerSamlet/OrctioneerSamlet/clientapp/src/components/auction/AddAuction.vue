<template>
  <div id="add-auction">
      <h1>Add a new auction</h1>
      <form v-if="!submitted">
          <label>Auction Title</label>
          <input type="text" v-model.lazy="auction.title" required />
          <label>Auction Description</label>
          <textarea v-model.lazy="auction.description"></textarea>
          <div id="cat-checkbox">
              <label>Category 1</label>
              <input type="checkbox" value="cat1" v-model="auction.categories" />
              <label>Category 2</label>
              <input type="checkbox" value="cat2" v-model="auction.categories" />
              <label>Category 3</label>
              <input type="checkbox" value="cat3" v-model="auction.categories" />
          </div>
          <input type="file" @change="onFileChanged" />
          <label>End date:</label>
          <select v-model="endDate">
              <option v-for="day in endDates" :key="day">{{day}}</option>
          </select>
          <button class="btn btn-secondary" @click.prevent="postAuction" @click="onUpload">Add auction</button>
      </form>

      <div v-show="submitted">
          <h3>Your auction has been posted!</h3>
      </div>
      <div id="preview">
          <h3>Preview Auction</h3>
          <p>Auction title {{ auction.title }}</p>
          <p>Auction content </p>
          <p>{{ auction.description }}</p>
          <p>Auction Categories:</p>
          <ul>
              <li v-for="cat in auction.categories" :key="cat">{{ cat }}</li>
          </ul>
          <p>Auction ends in: {{ auction.expirationDate }} </p>
      </div>
  </div>
</template>

<script>

import axios from 'axios';
//import moment from 'vue-moment';

export default {
    
    data(){
    return{
        auction:{
            title:"",
            description: "",
            categories:[],
            expirationDate:""
        },
        endDates:[1,2,7,14,28],
        submitted: false,
        selectedFile: null,
        endDate: "",
        picpath: ""
       
    }
    },
    methods:{
        postAuction: function () {
            var ref = this;
            this.calcEndDate();
            var pic = {};
            pic.ImageOfItem = "randomstring";

            
            axios.post('http://localhost:5000/api/ItemEntity/Item',
                {
                    Title: ref.auction.title,
                    BuyOutPrice: 123,
                    DescriptionOfItem: ref.auction.description,
                    ExpirationDate: ref.auction.expirationDate,
                    img: ref.picpath
                })
            //    .then(function (response) {
            //    console.log(response);
            //}).catch(function (error) {
            //    console.log(error);
            //});
            this.submitted = true;
        },
        onFileChanged(event) {
            var ref = this;
            ref.selectedFile = event.target.files[0];
        },
        onUpload() {
            var ref = this;
            var formData = new FormData();
            formData.append('file', ref.selectedFile, ref.selectedFile.name)
            ref.picpath = axios.post('http://localhost:5000/api/ItemEntity/CreateImage', formData);
        },
        calcEndDate() {
            const moment = this.$moment().add(this.endDate, 'days').format();
            window.console.log(this.endDate);
            this.auction.expirationDate = moment;
        },
    },
}
</script>

<style>
#add-auction *{
    box-sizing: border-box;
}

#add-auction{
    max-width: 800px;
    min-height: 1000px;
    margin: 0 auto;
}

label{
    display: block;
    margin: 20px 0 5px;
}

input[type="text"],textarea{
    display: block;
    width: 100%;
    padding: 8px;
}

#preview{
    padding: 10px 20px;
    border: 1px dotted #ccc;
    margin: 30px 0;
}

h3{
    margin-top: 10px;
}

#cat-checkbox input{
    display: inline-block;
    margin-right: 10px;
}

#cat-checkbox label{
    display: inline-block;
}
</style>
