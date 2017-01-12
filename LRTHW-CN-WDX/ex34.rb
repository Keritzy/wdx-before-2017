animals = ['bear', 'python', 'peacock', 'kangaroo', 'whale', 'platypus']

puts animals.first
puts animals.last
puts "size: %d" % animals.size


for i in (0..animals.size-1)
    puts "The {%d} is %s" % [i, animals[i]]
end
