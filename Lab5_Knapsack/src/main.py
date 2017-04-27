import itertools

def parseLine(line):
    strs = tuple(map(str.strip, line.split(',')))
    return (strs[0], *map(int, strs[1:]))

def main():
    path = input('Enter a path. (sample0.txt) ') or 'sample0.txt'
    maxWeight = 0
    maxItems = 0
    items = None
    
    try:
        with open(path) as file:
            lines = [line for line in file if line.strip() != '' and not line.strip().startswith('//')]
            maxWeight = int(lines[0])
            maxItems = int(lines[1])
            items = list(map(parseLine, lines[2:]))
    except:
        print('An Error occurred while reading input file.')
        return
    
    current = None
    currentWeight = -1
    currentValue = -1
    for count in range(1, maxItems + 1):
        for perm in itertools.combinations(items, count):
            weight = sum(item[1] for item in perm)
            if weight > maxWeight:
                continue
            
            value = sum(item[2] for item in perm)
            if value < currentValue:
                continue
            
            current = perm
            currentWeight = weight
            currentValue = value
    
    print('Best solution')
    print('weight', currentWeight)
    print('value', currentValue)
    print('items:')
    for item in current:
        print('   ' + str(item))

if __name__ == '__main__':
    main()
