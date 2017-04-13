$(document).ready(() => {
    let app = Sammy('#app', function () {
        this.get('#/', function (context) {
            console.log(context.verb);
        });

        this.get('#/pizzas', function (context) {
            console.log('Pizzas');
        });

        this.get('#/pizzas/:id', function (context) {
            console.log(context.params.id);
        });
    });

    app.run('#/');
});