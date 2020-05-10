<template>
  <div id="add-auction">
      <h1>Add a new auction</h1>
      <form v-if="!submitted">
          <label>Auction Title</label>
          <input type="text" v-model.lazy="auction.title" required/>
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
          <label>End date:</label>
          <select v-model="auction.endDate">
              <option v-for="day in endDates" :key="day">{{day}}</option>
          </select>
          <button class="btn btn-secondary" @click.prevent="postAuction">Add auction</button>
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
          <p>Auction ends in: {{ auction.endDate }} </p>
      </div>
  </div>
</template>

<script>

import axios from 'axios';
export default {
    
  data(){
    return{
        auction:{
            title:"",
            description: "",
            categories:[],
            endDate: ""
        },
        endDates:['1 day', '2 days', '7 days', '31 days'],
        submitted: false,
       
    }
  },
  methods:{
      postAuction: function () {
          var ref = this;
          axios.post('http://localhost:5000/Home/newItem',
              {
                  ItemId:99,
                  Title: ref.auction.title,
                  BuyOutPrice: 123,
                  ExpirationDate: "2020-05-012T10:21:47.6980913",
                  DateCreated:"2020-05-07T10:21:47.6980913",
                  UserIdSeller: 99,
                  DescriptionOfItem: ref.auction.description,
                  Sold: false,
                  Images: [],
                  Tags: [],
                  Bids: []
                
              })

            //    .then(function (response) {
            //    console.log(response);
            //}).catch(function (error) {
            //    console.log(error);
            //});
            this.submitted = true;
      }
  }
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
