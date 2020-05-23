<template>
  <div id="add-auction" :key="componentKey">
      <h1>Add a new auction</h1>
      <form v-if="!auctionSubmitted">
          <label>Auction Title</label>
          <input type="text" v-model.lazy="auction.title" required />
          <label>Auction Description</label>
          <textarea v-model.lazy="auction.description"></textarea>
          <input type="file" @change="onNewFileSelected" />
          <label>End date:</label>
          <select v-model="endDate" required>
              <option v-for="day in endDates" :key="day">{{day}}</option>
          </select>
          <button class="btn btn-secondary" @click.prevent="postNewAuction">Add auction</button>
      </form>

      <div v-show="auctionSubmitted">
          <h3>Your auction has been posted!</h3>
      </div>
  </div>
</template>

<script>
export default {
    
   data(){
        return {
            componentKey: 0,
            auction:{
                title:"",
                description: "",
                expirationDate:""
            },
            endDates: [1, 2, 7, 14, 28],
            endDate: ""

        }
    },
    computed: {
        picture() {
            return this.$store.state.picturePath;
        },
        auctionSubmitted() {
            return this.$store.state.auctionSubmitted;
        }

    },
    methods:{
        forceRerender() {
            this.componentKey += 1;
        },
        postNewAuction() {
            var payload = {
                title: this.auction.title,
                buyOutPrice: 123,
                description: this.auction.description,
                expDate: this.calcEndDate(),
                picturePath: this.picture
            }
            this.$store.dispatch('postNewAuction', payload);
            this.$store.dispatch('updateListing');
        },

        onNewFileSelected(event) {
            var formData = new FormData();
            var payload = {
                selectedFile: event.target.files[0],
                formData: formData
            }
            this.$store.dispatch('uploadFile', payload);
        },
        calcEndDate() {
            const moment = this.$moment().add(this.endDate, 'days').format();
            return moment;
        },

        },
        created() {
            this.forceRerender();
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
