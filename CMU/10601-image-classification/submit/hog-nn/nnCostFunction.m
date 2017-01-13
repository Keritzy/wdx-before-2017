function [J grad] = nnCostFunction(nn_params, ...
                                   input_layer_size, ...
                                   hidden_layer_size, ...
                                   num_labels, ...
                                   X, y, lambda)
                               
Theta1 = reshape(nn_params(1:hidden_layer_size * (input_layer_size + 1)), ...
                 hidden_layer_size, (input_layer_size + 1));

Theta2 = reshape(nn_params((1 + (hidden_layer_size * (input_layer_size + 1))):end), ...
                 num_labels, (hidden_layer_size + 1));

% Setup some useful variables
m = size(X, 1);
      

Theta1_grad = zeros(size(Theta1));
Theta2_grad = zeros(size(Theta2));

z2 = [ones(m,1),X] * Theta1'; % input to the hidden layer
a2 = sigmoid(z2);             % activation of hidden layer
z3 = [ones(m,1),a2] * Theta2';% input to the output layer
h  = sigmoid(z3); clear z3;   % output of the output layer

% error
Y = zeros(size(h));
for j = 1 : m
    Y(j,y(j)) = 1;
end
summat = -Y.*log(h) - (1-Y) .* log(1-h); clear Y;
J = 1 / m * sum(summat(:)); clear summat;

% regularization
Theta1sq = Theta1(:,2:end).^2;
Theta2sq = Theta2(:,2:end).^2;

J_reg = lambda/(2*m) * (sum(Theta1sq(:)) + sum(Theta2sq(:)));
clear Theta1sq Theta2sq;

J = J + J_reg;

% back propagation

for t = 1 : m
    delta_3 = h(t,:)';
    delta_3(y(t)) = delta_3(y(t)) - 1;
    % delta3 get!
    delta_2 = Theta2' * delta_3 .* sigmoidGradient([1,z2(t,:)]');
    % delta2 get!
    
    % accumulate Delta_3 and Delta_2
    Theta2_grad = Theta2_grad + delta_3 * [1,a2(t,:)];
    Theta1_grad = Theta1_grad + delta_2(2:end) * [1,X(t,:)];
end

Theta2_grad = Theta2_grad ./ m;
Theta1_grad = Theta1_grad ./ m;


Theta2_grad(:,2:end) = Theta2_grad(:,2:end) + lambda/m .* Theta2(:,2:end);
Theta1_grad(:,2:end) = Theta1_grad(:,2:end) + lambda/m .* Theta1(:,2:end);

% Unroll gradients
grad = [Theta1_grad(:) ; Theta2_grad(:)];


end
