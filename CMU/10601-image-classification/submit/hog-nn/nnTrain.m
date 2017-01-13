function [ Model ] = nnTrain( XTrain,YTrain )

input_layer_size  = size(XTrain,2);
hidden_layer_size = 25;
num_labels = size(unique(YTrain),1);

initial_Theta1 = randInitializeWeights(input_layer_size,hidden_layer_size);
initial_Theta2 = randInitializeWeights(hidden_layer_size,num_labels);


%{
initial_Theta1 = rand(hidden_layer_size, 1 + input_layer_size );
initial_Theta2 = rand(num_labels, 1 + hidden_layer_size );
%}

initial_nn_params = [initial_Theta1(:) ; initial_Theta2(:)];
options = optimset('Display','iter','GradObj', 'on','MaxIter', 50);

lambda = 1; % regularization parameter

costFunction = @(p) nnCostFunction(p, ...
                                   input_layer_size, ...
                                   hidden_layer_size, ...
                                   num_labels, XTrain, YTrain, lambda);

[nn_params, cost] = minFunc(costFunction, initial_nn_params, options);

Theta1 = reshape(nn_params(1:hidden_layer_size * (input_layer_size + 1)), ...
                 hidden_layer_size, (input_layer_size + 1));

Theta2 = reshape(nn_params((1 + (hidden_layer_size * (input_layer_size + 1))):end), ...
                 num_labels, (hidden_layer_size + 1));

Model.theta1 = Theta1;
Model.theta2 = Theta2;
             
end

