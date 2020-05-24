<template>
    <div id="uUser">
    <center><div class="col">
        <div class="row">
            <label>Navn: </label>
            <input style="padding-left: 5px"
            type="text"
            v-model="Name"/>
        </div>
        <div class="row">
            <label>Email: </label>
            <input style="padding-left: 5px"
            type="text"
            v-model="Email"/>
        </div>
        <div class="row">
            <label>Addresse: </label>
            <input style="padding-left: 5px"
            type="text"
            v-model="Address"/>
        </div>
        <div class="row">
            <label>by: </label>
            <input style="padding-left: 5px"
            type="text"
            v-model="City"/>
        </div>
        <div class="row">
            <label>Postnummer: </label>
            <input style="padding-left: 5px"
            type="text"
            v-model="Zipcode"/>
        </div>
        <div class="row">
            <label>Land: </label>
            <input style="padding-left: 5px"
                   type="text"
                   v-model="Contry"/>
        </div>
    </div></center>
        <div class="row">
            <button class="btn btn-outline-primary"
            v-on:click="updateUser">Opdater bruger</button>
            <button class="btn btn-outline-primary"
                    v-on:click="DeleteUser"
            >Slet bruger</button>
        </div>
        <span v-show="submitted" style="background: springgreen">Bruger er opdateret</span>
    </div>
</template>

<script>
    import axios from 'axios';
    import logout from '../Utility/Signout'
    export default {
        data(){
            return{
                Name:'',
                Email:'',
                Address:'',
                City: '',
                Zipcode: '',
                Contry: '',
                submitted: false
            }
        },
        created: function(){
            var ref = this;
            axios.get('http://localhost:5000/api/User/UserDetails')
                .then(function(response){
                    if(response.status !== 200){
                        window.console.log("Err" + response.status);
                    }
                    ref.Name = response.data.name;
                    ref.Email = response.data.email;
                    ref.Address = response.data.address;
                    ref.City = response.data.city;
                    ref.Zipcode = response.data.zipcode;
                    ref.Contry = response.data.contry;
                }).catch(function(err){
                window.console.log(err);
            });
        },
        methods:{
          updateUser(){
              var ref = this;
              const names = this.Name.split(' ');
              var user = {};
              user.FirstName = names[0];
              user.LastName = names[1];
              user.Address = {
                  Contry : this.Contry,
                  City : this.City,
                  Zipcode : this.Zipcode,
                  Address : this.Address
              };
              axios.post('http://localhost:5000/api/User/UpdateUser',user)
                  .then(function(response){
                  if(response.status !== 200)
                  {
                      window.console.log("Something went wrong");
                      return;
                  }
                  axios.post('http://localhost:5000/api/Signup/Update',
                      {
                          UserN : null,
                          Email : ref.Email,
                          Password : null
                      }).then(function(){
                          ref.submitted = true;
                  }).catch(function(err){
                      window.console.log(err);
                  })
              }).catch(function(err){
                  window.console.log(err);
              })
          },
          DeleteUser(){
              var ref = this;
              axios.delete('http://localhost:5000/api/User/User')
              .then(function(response){
                  if(response.status !== 200)
                  {
                      window.console.log("Error");
                  }
                  axios.delete('http://localhost:5000/api/Signup')
                  .then(function(response){
                      if(response.status !== 200)
                      {
                          window.console.log("Error");
                      }
                      ref.$emit(logout.methods.logout());
                      ref.$router.push({name:'Home'});
                  })
              })
          }
        }
    }
</script>

<style scoped>
    #uUser{
        max-width: 800px;
        min-height: 1000px;
        margin: 0 auto;
    }
</style>