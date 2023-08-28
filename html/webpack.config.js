const path = require("path");

module.exports = {
    module: {
        rules: [
            // Configurar la regla para procesar arhcivos scss
            {
                test: /\.(scss)$/,
                use: [
                    {
                        loader: "style-loader"
                    },
                    {
                        loader: "css-loader"
                    },
                    {
                        loader: "sass-loader"
                    },
                ]
            }
        ] 
    }
}