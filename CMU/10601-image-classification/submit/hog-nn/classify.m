function Y = classify(Model,xTest)
  xTestFea = extractFea(xTest);
  
  % Y = nbClassify(Model,xTestFea);
  
  % [ Y ] = sfmClassify( Model, xTestFea' ) - 1;
  
  [ Y ] = nnClassify( Model, xTestFea )-1;
  end
